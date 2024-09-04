using CommonTestUtilities.Request.Product;
using CommonTestUtilities.Request.User;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApiTest
{
   public class RegisterUseTest : IClassFixture<DataBaseInMemoryApi>
    {
        private readonly HttpClient _httpClient;

        public RegisterUseTest(DataBaseInMemoryApi factory) => _httpClient = factory.CreateClient();

        [Fact]
        public async Task Success()
        {
            var request = RegisterUserRequestBuilder.Create();
            var response = await _httpClient.PostAsJsonAsync("users", request);
            var streamReadLine = await response.Content.ReadAsStreamAsync();
            var responseDataJson = await JsonDocument.ParseAsync(streamReadLine);
            responseDataJson.RootElement.GetProperty("name").GetString().Should().NotBeNull().And.Be(request.Name);

        }

        [Fact]
        public async Task Error_EmtpyName()
        {
            var request = RegisterUserRequestBuilder.Create();
            request.Name = string.Empty;
            var response = await _httpClient.PostAsJsonAsync("users", request);
            var readStream = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonDocument.ParseAsync(readStream);
            var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();
            errors.Should().ContainSingle();
        }
    }
}
