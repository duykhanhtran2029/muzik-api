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
using MusicPlayer.Servcies;

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
        [HttpGet]
        public async Task<List<RecognizableSong>> Get() => await _songSvc.GetAsync();

        [HttpGet("{songID}")]
        public async Task<ActionResult<RecognizableSong>> Get(uint songID)
        {
            var song = await _songSvc.GetAsync(songID);
            if (song is null)
                return NotFound();
            return song;
        }

        [HttpPost("FingerPrinting")]
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

            return this.Ok(jsonResult);
        }

        [HttpDelete("{songID}")]
        public async Task<IActionResult> Delete(uint songID)
        {
            var song = await _songSvc.GetAsync(songID);

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
