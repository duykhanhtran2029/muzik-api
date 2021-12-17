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
        public IEnumerable<Song> Get() => _songsSvc.Get();

        [HttpGet("{songID}")]
        public Song Get([FromRoute] uint songID) => _songsSvc.Get(songID);

        [HttpPost("FingerPrinting")]
        public void FingerPrinting()
        {
            var file = new MemoryStream();
            try
            {
                Request.Form.Files[0].CopyTo(file);
                byte[] data = file.ToArray();
                _songsSvc.FingerPrinting(data);
            }
            catch
            {

            }
            
        }
    }
}
