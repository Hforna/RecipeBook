using CommonTestUtilities.AutoMapperBuilder;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using ProjectAspNet.Domain.Entities.Recipes;
using ProjectAspNet.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTest
{
    public class DeleteRecipeTest : IClassFixture<DataBaseInMemoryApi>
    {
        private readonly HttpClient _httpClient;
        private readonly Guid _userIdentifier;
        private readonly long _recipeId;

        public DeleteRecipeTest(DataBaseInMemoryApi factory)
        {
            _httpClient = factory.CreateClient();
            _userIdentifier = factory.getUserIdentifier();
            _recipeId = factory.getRecipeId();
        }

        [Fact]
        public async Task Success()
        {
            var sqIds = sqIdsBuilder.Build();
            var encodeId = sqIds.Encode(_recipeId);
            var generateToken = JwtTokenGenerate.Build();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", generateToken.Generate(_userIdentifier));
            var response = await _httpClient.DeleteAsync($"recipe/{encodeId}");
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var getResponse = await _httpClient.GetAsync($"recipe/{encodeId}");
            getResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Error_No_Recipe()
        {
            var sqIds = sqIdsBuilder.Build();
            var encodeId = sqIds.Encode(_recipeId);
            var generateToken = JwtTokenGenerate.Build();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", generateToken.Generate(_userIdentifier));
            var response = await _httpClient.DeleteAsync($"recipe/asdFD");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            
        }
    }
}
