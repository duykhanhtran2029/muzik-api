using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using System;

namespace AzureHelper
{
    internal class Program
    {
        static BlobServiceClient blobServiceClient;
        const string connectionString = "DefaultEndpointsProtocol=https;AccountName=muzik;AccountKey=FAn5LyjNIntCw0UD4bs6gEmYHwW7wKM4KeN0sEkT0g4MuwI2noyE/2fLV2OYVBFfAF0JpgS2iNNN+AStW7AeTQ==;EndpointSuffix=core.windows.net";
        const string accoutName = "muzik";
        const string accountKey = "FAn5LyjNIntCw0UD4bs6gEmYHwW7wKM4KeN0sEkT0g4MuwI2noyE/2fLV2OYVBFfAF0JpgS2iNNN+AStW7AeTQ==";
        static void Main(string[] args)
        {
            blobServiceClient = new BlobServiceClient(connectionString);
            var containers = blobServiceClient.GetBlobContainers();
            foreach (var container in containers)
            {
                GetServiceSasTokenForContainer(container.Name);
            }
        }

        static void ChangeAPIVer(string APIVer = "2020-04-08")
        {
            var props = blobServiceClient.GetProperties();
            props.Value.DefaultServiceVersion = APIVer;
            blobServiceClient.SetProperties(props);
            Console.WriteLine($"Change API version to {APIVer}");
        }

        static void DeleteAllBlob(string containerName)
        {
            var blobContainerClient = blobServiceClient.GetBlobContainerClient("images");
            int count = 0;
            var allBlobs = blobContainerClient.GetBlobs();
            foreach (var blob in allBlobs)
            {
                count++;
                var blobClient = blobContainerClient.GetBlobClient(blob.Name);
                blobClient.Delete();
            }
            Console.WriteLine($"Deleted {count} files");
        }

        static void GetServiceSasTokenForContainer(string containerName, string storedPolicyName = null)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            if (containerClient.CanGenerateSasUri)
            {
                BlobSasBuilder sasBuilder = new BlobSasBuilder()
                {
                    BlobContainerName = containerClient.Name,
                    Resource = "c"
                };

                if (storedPolicyName == null)
                {
                    sasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddYears(1);
                    sasBuilder.SetPermissions(BlobContainerSasPermissions.All);
                }
                else
                {
                    sasBuilder.Identifier = storedPolicyName;
                }

                var sasToken = sasBuilder.ToSasQueryParameters(new StorageSharedKeyCredential(accoutName, accountKey)).ToString();
                Console.WriteLine("{0}_SAS: '{1}',", containerName.ToUpper(), sasToken);
            }
            else
            {
                Console.WriteLine(@"BlobContainerClient must be authorized with Shared Key credentials to create a service SAS.");
            }
        }
    }
}
