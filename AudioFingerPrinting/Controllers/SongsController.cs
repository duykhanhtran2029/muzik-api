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

namespace AudioFingerPrinting.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongsController : ControllerBase
    {
        private readonly SongsSvc _songsSvc;

        public SongsController(SongsSvc songsSvc)
        {
            _songsSvc = songsSvc;
        }

        [HttpGet]
        public async Task<List<Song>> Get() => await _songsSvc.GetAsync();

        [HttpGet("{songID}")]
        public async Task<ActionResult<Song>> Get (uint songID)
        {
            var song = await _songsSvc.GetAsync(songID);
            if (song is null)
                return NotFound();
            return song;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Song newSong)
        {
            await _songsSvc.CreateAsync(newSong);

            return CreatedAtAction(nameof(Get), new { id = newSong.Id }, newSong);
        }

        [HttpPut("{songID}")]
        public async Task<IActionResult> Update(uint songID, Song updatedSong)
        {
            var song = await _songsSvc.GetAsync(songID);

            if (song is null)
            {
                return NotFound();
            }

            updatedSong.Id = song.Id;

            await _songsSvc.UpdateAsync(songID, updatedSong);

            return NoContent();
        }

        [HttpDelete("{songID}")]
        public async Task<IActionResult> Delete(uint songID)
        {
            var song = await _songsSvc.GetAsync(songID);

            if (song is null)
            {
                return NotFound();
            }

            await _songsSvc.RemoveAsync(song.Id);

            return NoContent();
        }

        [HttpPost("FingerPrinting")]
        public IActionResult FingerPrinting()
        {
            var file = new MemoryStream();
            Request.Form.Files[0].CopyTo(file);
            byte[] data = file.ToArray();
            FingerPrinting_ResultDTO result = Startup.recognizer.Recognizing(data);
            var jsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return this.Ok(jsonResult);
        }
    }
}
