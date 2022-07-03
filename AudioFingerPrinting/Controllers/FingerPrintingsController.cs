using Database;
using AudioFingerPrinting.Servcies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using AudioFingerPrinting.DTO;
using System.Text.Json;
using Newtonsoft.Json;
using CoreLib.AudioProcessing.Server;
using CoreLib.AudioFormats;
using NAudio.Wave;
using CoreLib.Tools;
using CoreLib;
using Database.AudioFingerPrinting;

namespace AudioFingerPrinting.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FingerPrintingsController : ControllerBase
    {
        private readonly BlobSvc _blobStorageSvc;
        private readonly IAzureStorageSettings _settings;
        private readonly SongSvc _songSvc;
        public FingerPrintingsController(
            BlobSvc blobStorageSvc,
            IAzureStorageSettings settings,
            SongSvc songSvc
            )
        {
            _blobStorageSvc = blobStorageSvc;
            _settings = settings;
            _songSvc = songSvc;
        }

        [HttpGet("{songName}")]
        public async Task<IActionResult> Post(string songName)
        {
            var song = await _songSvc.GetSongByNameAsync(songName);
            if (song != null)
            {
                return NoContent();
            }
            RecognizableSong newSong = new RecognizableSong(_songSvc.newId(), songName);
            _songSvc.CreateAsync(newSong);

            string path = await _blobStorageSvc.GetFileBlobAsync(_settings.SongsContainer, newSong.Name + ".mp3");
            MemoryStream stream = AudioReader.WavConverter(path);
            _songSvc.AddNewSong(stream.ToArray(), newSong.Id);
            stream.Dispose();

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessRecord([FromBody] Record_RequestDTO request)
        {
            string path = await _blobStorageSvc.GetFileBlobAsync(_settings.RecordsContainer, request.FileName);
            MemoryStream stream = AudioReader.WavConverter(path);
            byte[] data = stream.ToArray();
            FingerPrintingResult result = Startup.recognizer.Recognizing(data);
            var jsonResult = JsonConvert.SerializeObject(result);

            stream.Dispose();
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            await _blobStorageSvc.DeleteFileBlobAsync(_settings.RecordsContainer, request.FileName);

            return Ok(jsonResult);
        }

        [HttpDelete("{songName}")]
        public async Task<IActionResult> Delete(string songName)
        {
            var song = await _songSvc.GetSongByNameAsync(songName);

            if (song is null)
            {
                return NotFound();
            }

            try
            {
                await _songSvc.RemoveAsync(song.Id);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(song);
        }
    }
}
