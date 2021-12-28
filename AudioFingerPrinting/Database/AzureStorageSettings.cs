using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioFingerPrinting.Database
{
    public class AzureStorageSettings : IAzureStorageSettings
    {
        public string ConnectionString { get; set; }
        public string BaseURL { get; set; }
        public string SongsContainer { get; set; }
        public string ImagesContainer { get; set; }
        public string RecordsContainer { get; set; }
    }
    public interface IAzureStorageSettings
    {
        string ConnectionString { get; set; }
        string BaseURL { get; set; }
        string SongsContainer { get; set; }
        string ImagesContainer { get; set; }
        string RecordsContainer { get; set; }
    }
}
