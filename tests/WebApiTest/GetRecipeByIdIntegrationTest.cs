using CommonTestUtilities.AutoMapperBuilder;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApiTest
{
    public class GetRecipeByIdIntegrationTest : IClassFixture<DataBaseInMemoryApi>
    {
        private readonly HttpClient _httpClient;
        private readonly Guid _getUserIdentifier;
        private readonly long _recipeId;
        private readonly string _recipeName;

        public GetRecipeByIdIntegrationTest(DataBaseInMemoryApi factory)
        {
            _httpClient = factory.CreateClient();
            _getUserIdentifier = factory.getUserIdentifier();
            _recipeId = factory.getRecipeId();
            _recipeName = factory.getRecipeName();
        }

        [Fact]
        public async Task Success()
        {
            var generateToken = JwtTokenGenerate.Build();
            var sqlIds = sqIdsBuilder.Build();
            var idEncode = sqlIds.Encode(_recipeId);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", generateToken.Generate(_getUserIdentifier));
            var response = await _httpClient.GetAsync($"recipe/{idEncode}");
            var readResponse = await response.Content.ReadAsStreamAsync();
            var responseContent = JsonDocument.Parse(readResponse);
            responseContent.RootElement.GetProperty("title").GetString().Should().Be(_recipeName);
        }
    }
}
