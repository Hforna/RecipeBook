using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProjectAspNet.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApiTest
{
    public class TokenAcceptedTest : IClassFixture<DataBaseInMemoryApi>, IAsyncDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly Guid _userIdentifier;
        private readonly string _name;
        private readonly ProjectAspNetDbContext _dbContext;

        public TokenAcceptedTest(DataBaseInMemoryApi factory)
        {
            _httpClient = factory.CreateClient();
            _userIdentifier = factory.getUserIdentifier();
            _name = factory.getUsername();
            _dbContext = factory.DbContext;
        }

        public async ValueTask DisposeAsync()
        {
            await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM ingredients");
            await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM instructions");
            await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM recipes");
            await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM dishtype");
            await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM users");
        }

        [Fact]
        public async Task Success()
        {
            var generate = JwtTokenGenerate.Build().Generate(_userIdentifier);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", generate);
            var request = await _httpClient.GetAsync("user");
            var response = request.Content.ReadAsStream();
            var toJson = await JsonDocument.ParseAsync(response);
            toJson.RootElement.GetProperty("name").GetString().Should().Be(_name);
        }
    }
}
