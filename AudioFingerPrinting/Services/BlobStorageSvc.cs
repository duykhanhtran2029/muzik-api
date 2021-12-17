using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioFingerPrinting.Servcies
{
    public class BlobStorageSvc
    {
        public static async Task<CloudBlobContainer> GetCloudBlogContainer()
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=shazam;AccountKey=E55FKCcPg0WMFzOkES0SEk8GavZWm89BmOh4ahWSU8Ybf2Danqu4KzVr2EuEI284LY5I0XxPo4pEoXSfTqPKhg==;EndpointSuffix=core.windows.net";

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference("shazam");

            CloudBlockBlob blob = container.GetBlockBlobReference("BrownNoise.wav");
            await blob.FetchAttributesAsync();
            byte[] byteArray = new byte[blob.Properties.Length];
            await blob.DownloadToByteArrayAsync(byteArray, 0);
            return container;
        }
    }
}
