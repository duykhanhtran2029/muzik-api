using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioFingerPrinting.DTO
{
    public class Record_RequestDTO
    {
        [JsonProperty("fileName")]
        public string FileName { get; set; }
    }
}
