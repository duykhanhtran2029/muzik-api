using AudioFingerPrinting.Database;
using AudioFingerPrinting.Servcies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioFingerPrinting.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FingerprintsController : ControllerBase
    {
        private readonly FingerprintsSvc _fingerprintsSvc;

        public FingerprintsController(FingerprintsSvc fingerprintsSvc)
        {
            _fingerprintsSvc = fingerprintsSvc;
        }

        [HttpGet]
        public IEnumerable<Fingerprint> Get() => _fingerprintsSvc.Get();

        [HttpGet("{songID}")]
        public Fingerprint Get([FromRoute] uint songID) => _fingerprintsSvc.Get(songID);
    }
}
