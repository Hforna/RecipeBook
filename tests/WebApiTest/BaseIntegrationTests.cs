using ProjectAspNet.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApiTest
{
    public class BaseIntegrationTests<T> : IClassFixture<ProjectAspNetDbContext>
    {
        private string method;
        private readonly HttpClient _httpClient;
        public BaseIntegrationTests(DataBaseInMemoryApi factory, string method)
        {
            _httpClient = factory.CreateClient();
            this.method = method;
        }

        public async Task<JsonDocument> BaseClass(T request)
        {
            var response = await _httpClient.PostAsJsonAsync(method, request);
            await using var readAsStream = await response.Content.ReadAsStreamAsync();
            return await JsonDocument.ParseAsync(readAsStream);
        }
    }
}
