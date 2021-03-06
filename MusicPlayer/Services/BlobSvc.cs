using Database;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MusicPlayer.Servcies
{
    public class BlobSvc
    {
        private  BlobServiceClient _blobServiceClient;

        public BlobSvc(IAzureStorageSettings settings)
        {
            _blobServiceClient = new BlobServiceClient(settings.ConnectionString);
        }

        public async Task<Uri> UploadFileBlobAsync(string blobContainerName, Stream content, string contentType, string fileName)
        {
            var containerClient = GetContainerClient(blobContainerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            var props = blobClient.GetProperties();
            await blobClient.UploadAsync(content, new BlobHttpHeaders { ContentType = contentType });
            return blobClient.Uri;
        }

        public async Task<string> GetFileBlobAsync(string blobContainerName, string fileName)
        {
            var containerClient = GetContainerClient(blobContainerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            string path = new FileInfo("Resouces/" + fileName).FullName;
            if(File.Exists(path))
            {
                File.Delete(path);
            }
            FileStream stream = new FileStream(path, FileMode.CreateNew, FileAccess.ReadWrite);
            await blobClient.DownloadToAsync(stream);
            stream.Dispose();
            return path;
        }

        public async Task DeleteFileBlobAsync(string blobContainerName, string fileName)
        {
            var containerClient = GetContainerClient(blobContainerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.DeleteAsync();
        }

        private BlobContainerClient GetContainerClient(string blobContainerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName);
            containerClient.CreateIfNotExists(PublicAccessType.Blob);
            return containerClient;
        }
    }
}
