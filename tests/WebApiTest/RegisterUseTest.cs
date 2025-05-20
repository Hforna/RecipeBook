using CommonTestUtilities.Request.Product;
using CommonTestUtilities.Request.User;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
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
    public class RegisterUseTest : IClassFixture<DataBaseInMemoryApi>, IAsyncDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly ProjectAspNetDbContext _dbContext;

        public RegisterUseTest(DataBaseInMemoryApi factory)
        {
            _httpClient = factory.CreateClient();
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
            var request = RegisterUserRequestBuilder.Create();
            var response = await _httpClient.PostAsJsonAsync("user", request);
            var streamReadLine = await response.Content.ReadAsStreamAsync();
            var responseDataJson = await JsonDocument.ParseAsync(streamReadLine);
            responseDataJson.RootElement.GetProperty("name").GetString().Should().NotBeNull().And.Be(request.Name); 

        }

        [Fact]
        public async Task Error_EmtpyName()
        {
            var request = RegisterUserRequestBuilder.Create();
            request.Name = string.Empty;
            var response = await _httpClient.PostAsJsonAsync("user", request);
            var readStream = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(readStream);
            var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();
            errors.Should().ContainSingle();
        }
    }
}
