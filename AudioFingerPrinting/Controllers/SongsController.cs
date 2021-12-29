using AudioFingerPrinting.Database;
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

namespace AudioFingerPrinting.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongsController : ControllerBase
    {
        private readonly SongsSvc _songsSvc;
        private readonly BlobSvc _blobStorageSvc;
        private readonly IAzureStorageSettings _settings;
        public SongsController(SongsSvc songsSvc, BlobSvc blobStorageSvc, IAzureStorageSettings settings)
        {
            _songsSvc = songsSvc;
            _blobStorageSvc = blobStorageSvc;
            _settings = settings;
        }

        [HttpGet]
        public async Task<List<Song>> Get() => await _songsSvc.GetAsync();

        [HttpGet("{songID}")]
        public async Task<ActionResult<Song>> Get(uint songID)
        {
            var song = await _songsSvc.GetAsync(songID);
            if (song is null)
                return NotFound();
            return song;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Song newSong)
        {
            newSong.Id = _songsSvc.newId();
            _songsSvc.CreateAsync(newSong);
            
            var fileName = newSong.Link.Replace($"{_settings.BaseURL}/{_settings.SongsContainer}/", "");
            string path = await _blobStorageSvc.GetFileBlobAsync(_settings.SongsContainer, fileName);
            MemoryStream stream = AudioReader.WavConverter(path);
            _songsSvc.AddNewSong(stream.ToArray(), newSong.Id);
            stream.Dispose();

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            return Ok(newSong);
        }

        [HttpPut("{songID}")]
        public async Task<IActionResult> Update(Song updatedSong)
        {
            var song = await _songsSvc.GetAsync(updatedSong.Id);

            if (song is null)
            {
                return NotFound();
            }

            updatedSong.Id = song.Id;

            try
            {
                await _songsSvc.UpdateAsync(updatedSong.Id, updatedSong);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(updatedSong);
        }

        [HttpDelete("{songID}")]
        public async Task<IActionResult> Delete(uint songID)
        {
            var song = await _songsSvc.GetAsync(songID);

            if (song is null)
            {
                return NotFound();
            }

            try
            {
                await _songsSvc.RemoveAsync(song.Id);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(song);
        }


        [HttpPost("FingerPrinting")]
        public async Task<IActionResult> ProcessRecord([FromBody] Record_RequestDTO request)
        {

            string path = await _blobStorageSvc.GetFileBlobAsync(_settings.RecordsContainer, request.FileName);
            MemoryStream stream = AudioReader.WavConverter(path);
            byte[] data = stream.ToArray();
            FingerPrinting_ResultDTO result = Startup.recognizer.Recognizing(data);
            var jsonResult = JsonConvert.SerializeObject(result);


            stream.Dispose();
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            await _blobStorageSvc.DeleteFileBlobAsync(_settings.RecordsContainer, request.FileName);

            return this.Ok(jsonResult);
        }
    }
}
