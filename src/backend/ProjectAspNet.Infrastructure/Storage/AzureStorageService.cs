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
        public Task Upload(UserEntitie user, Stream file, string fileName)
        {
            return Task.CompletedTask;
        }
    }
}
