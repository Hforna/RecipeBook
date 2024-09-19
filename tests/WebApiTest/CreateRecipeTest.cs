using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.Recipe;
using FluentAssertions;
using ProjectAspNet.Communication.Requests.Enums;
using ProjectAspNet.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApiTest
{
    public class CreateRecipeTest : IClassFixture<DataBaseInMemoryApi>
    {
        private readonly HttpClient _httpClient;
        private readonly Guid _userIdentifier;

        public CreateRecipeTest(DataBaseInMemoryApi factory)
        {
            _httpClient = factory.CreateClient();
            _userIdentifier = factory.getUserIdentifier();
        }

        [Fact]
        public async Task Success()
        {
            var generateToken = JwtTokenGenerate.Build();
            var request = RequestRecipeBuild.Build();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", generateToken.Generate(_userIdentifier));
            var response = await _httpClient.PostAsJsonAsync("recipe", request);
            var readResponse = await response.Content.ReadAsStreamAsync();
            var responseContent = JsonDocument.Parse(readResponse);
            responseContent.RootElement.GetProperty("title").GetString().Should().Be(request.Title);
        }

        [Fact]
        public async Task Error_Invalid_DishType_Out_Range()
        {
            var generateToken = JwtTokenGenerate.Build();
            var request = RequestRecipeBuild.Build();
            request.DishTypes = [(DishType) 4, (DishType) 10];
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", generateToken.Generate(_userIdentifier));
            var response = await _httpClient.PostAsJsonAsync("recipe", request);
            var readResponse = await response.Content.ReadAsStreamAsync();
            var responseContext = JsonDocument.Parse(readResponse);

            responseContext.RootElement.GetProperty("errors").EnumerateArray().Should().ContainSingle(ResourceExceptMessages.DISH_TYPE_OUT_ENUM);
        }
    }
}
