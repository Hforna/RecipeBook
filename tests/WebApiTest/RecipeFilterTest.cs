using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.Recipe;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApiTest
{
    public class RecipeFilterTest : IClassFixture<DataBaseInMemoryApi>
    {
        private readonly HttpClient _httpClient;
        private readonly Guid _userIdentifier;
        private readonly string _recipeTitle;

        public RecipeFilterTest(DataBaseInMemoryApi factory)
        {
            _httpClient = factory.CreateClient();
            _userIdentifier = factory.getUserIdentifier();
            _recipeTitle = factory.getRecipeName();
        }

        [Fact]
        public async Task Success()
        {
            var tokenGenerate = JwtTokenGenerate.Build();
            var request = RequestFilterRecipeBuild.Build();
            request.TitleIngredientsRecipe = _recipeTitle;

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenGenerate.Generate(_userIdentifier));
            var response = await _httpClient.PostAsJsonAsync("recipe/filters", request);
            var readResponse = await response.Content.ReadAsStreamAsync();
            var responseContent = JsonDocument.Parse(readResponse);
            var listRecipe = responseContent.RootElement.GetProperty("recipe").EnumerateArray();

            foreach (var recipe in listRecipe)
            {
                var title = recipe.GetProperty("title").GetString();
                title.Should().NotBeNullOrWhiteSpace();
                title.Should().Be(request.TitleIngredientsRecipe);
            }
        }

        [Fact]
        public async Task Error_No_Recipe()
        {
            var tokenGenerate = JwtTokenGenerate.Build();
            var request = RequestFilterRecipeBuild.Build();
            request.Difficulty = [];
            request.CookingTime = [];
            request.DishTypes = [];
            
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenGenerate.Generate(_userIdentifier));
            var response = await _httpClient.PostAsJsonAsync("recipe/filters", request);
            var readResponse = await response.Content.ReadAsStreamAsync();
            var responseContent = JsonDocument.Parse(readResponse);

            var arrayRecipe = responseContent.RootElement.GetProperty("recipe").EnumerateArray();
            arrayRecipe.Should().BeEmpty();

        }
    }
}
