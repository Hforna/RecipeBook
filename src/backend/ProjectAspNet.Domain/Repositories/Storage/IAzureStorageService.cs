using ProjectAspNet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Repositories.Storage
{
    public interface IAzureStorageService
    {
        public Task Upload(UserEntitie user, Stream file, string fileName);

        public Task<string> GetFileUrl(UserEntitie user, string fileName);

        public Task Delete(UserEntitie user, string fileName);

        public Task DeleteUser(Guid uid);
    }
}
