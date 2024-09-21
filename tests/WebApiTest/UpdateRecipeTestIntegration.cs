using CommonTestUtilities.AutoMapperBuilder;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.Recipe;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using ProjectAspNet.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApiTest
{
    public class UpdateRecipeTestIntegration : IClassFixture<DataBaseInMemoryApi>
    {
        private readonly HttpClient _httpClient;
        private readonly Guid _userIdentifier;
        private readonly string _name;
        private readonly long _recipeId;

        public UpdateRecipeTestIntegration(DataBaseInMemoryApi factory)
        {
            _httpClient = factory.CreateClient();
            _userIdentifier = factory.getUserIdentifier();
            _name = factory.getUsername();
            _recipeId = factory.getRecipeId();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestRecipeBuild.Build();
            var createToken = JwtTokenGenerate.Build().Generate(_userIdentifier);
            var encodeId = sqIdsBuilder.Build().Encode(_recipeId);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", createToken);
            var response = await _httpClient.PutAsJsonAsync($"recipe/{encodeId}", request);
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var getResponse = await _httpClient.GetAsync($"recipe/{encodeId}");
            var readResponse = getResponse.Content.ReadAsStream();
            var contentResponse = JsonDocument.Parse(readResponse);

            contentResponse.RootElement.GetProperty("title").GetString().Should().Be(request.Title);
        }

        [Fact]
        public async Task No_Recipe_Found()
        {
            var request = RequestRecipeBuild.Build();
            var createToken = JwtTokenGenerate.Build();
            var encodeId = sqIdsBuilder.Build().Encode(_recipeId);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", createToken.Generate(_userIdentifier));
            var response = await _httpClient.PutAsJsonAsync("recipe/ASdD", request);
            var readResponse = response.Content.ReadAsStream();
            var responseContent = JsonDocument.Parse(readResponse);

            responseContent.RootElement.GetProperty("errors").EnumerateArray().Should().ContainSingle(ResourceExceptMessages.NO_RECIPE_FOUND);

        }
    }
}
