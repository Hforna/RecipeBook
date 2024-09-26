using Azure.Storage.Blobs;
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

        public async Task Upload(UserEntitie user, Stream file, string fileName)
        {
            var container = _blobClient.GetBlobContainerClient(user.UserIdentifier.ToString());
            await container.CreateIfNotExistsAsync();

            var blobClient = container.GetBlobClient(fileName);
            await  blobClient.UploadAsync(file, overwrite: true);
        }
    }
}
