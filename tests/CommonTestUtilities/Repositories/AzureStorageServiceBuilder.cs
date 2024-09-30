using Bogus;
using Moq;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Repositories.Storage;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Repositories
{
    public class AzureStorageServiceBuilder
    {
        private readonly Mock<IAzureStorageService> _storageService = new Mock<IAzureStorageService>();

        public IAzureStorageService Build()
        {
            return _storageService.Object;
        }

        public void GetFileUrlMock(UserEntitie user, string fileName)
        {
            var image = new Faker().Image.LoremFlickrUrl();

            _storageService.Setup(d => d.GetFileUrl(user, fileName)).ReturnsAsync(image);
        }
    }
}
