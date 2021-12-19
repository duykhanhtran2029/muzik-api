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
        public IActionResult GetAllSongs()
        {
            IList<Song> listSongs = _songsSvc.GetAll();
            if (listSongs == null || !listSongs.Any())
                return this.NoContent();
            return this.Ok(listSongs);
        }

        [HttpGet("{songID}")]
        public Song Get([FromRoute] uint songID) => _songsSvc.GetById(songID);

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
