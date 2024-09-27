using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Repositories.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Infrastructure.Storage
{
    public class AzureStorageService : IAzureStorageService
    {
        private readonly BlobServiceClient _blobClient;

        public AzureStorageService(BlobServiceClient blobClient)
        {
            _blobClient = blobClient;
        }

        public async Task<string> GetFileUrl(UserEntitie user, string fileName)
        {
            var container = _blobClient.GetBlobContainerClient(user.UserIdentifier.ToString());
            var exists = await container.ExistsAsync();

            if(exists.Value == false)
                return string.Empty;

            var blobClient = container.GetBlobClient(fileName);
            exists = await blobClient.ExistsAsync();

            if(exists.Value)
            {
                var sasBuilder = new BlobSasBuilder()
                {
                    BlobContainerName = container.Name,
                    ExpiresOn = DateTime.UtcNow.AddMinutes(40),
                    Resource = "b",
                    BlobName = fileName,
                };

                sasBuilder.SetPermissions(BlobSasPermissions.Read);

                return blobClient.GenerateSasUri(sasBuilder).ToString();
            }

            return string.Empty;
        }

        public async Task Upload(UserEntitie user, Stream file, string fileName)
        {
            var container = _blobClient.GetBlobContainerClient(user.UserIdentifier.ToString());
            await container.CreateIfNotExistsAsync();

            var blobClient = container.GetBlobClient(fileName);
            await  blobClient.UploadAsync(file, overwrite: true);
        }
    }
}
