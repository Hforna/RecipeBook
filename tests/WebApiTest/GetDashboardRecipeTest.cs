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
    public class GetDashboardRecipeTest : IClassFixture<DataBaseInMemoryApi>
    {
        private readonly HttpClient _httpClient;
        private readonly Guid _userIdentifier;

        public GetDashboardRecipeTest(DataBaseInMemoryApi factory)
        {
            _httpClient = factory.CreateClient();
            _userIdentifier = factory.getUserIdentifier();
        }
        [Fact]
        public async Task Success()
        {
            var generateToken = JwtTokenGenerate.Build();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", generateToken.Generate(_userIdentifier));

            var response = await _httpClient.GetAsync("recipe/dashboard");
            var readResponse = await response.Content.ReadAsStreamAsync();
            var responseContent = JsonDocument.Parse(readResponse);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            responseContent.Should().NotBeNull();
        }

        [Fact]
        public async Task No_Token()
        {
            var generateToken = JwtTokenGenerate.Build();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", generateToken.Generate(Guid.NewGuid()));

            var response = await _httpClient.GetAsync("recipe/dashboard");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }
    }
}
