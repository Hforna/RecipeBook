using CommonTestUtilities.Request.Product;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using ProjectAspNet.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApiTest.InLineClasses;

namespace WebApiTest
{
    public class RegisterProductTest : IClassFixture<DataBaseInMemoryApi>
    {
        private readonly HttpClient _httpClient;

        public RegisterProductTest(DataBaseInMemoryApi factory) => _httpClient = factory.CreateClient();

        [Fact]
        public async Task Success()
        {
            var request = RegisterProductRequestBuilder.Create();
            var response = await _httpClient.PostAsJsonAsync("products", request);
            await using var readStream = await response.Content.ReadAsStreamAsync();
            var contentResponse = await JsonDocument.ParseAsync(readStream);
            contentResponse.RootElement.GetProperty("productName").ToString().Should().Be(request.ProductName);
        }

        [Theory]
        [ClassData(typeof(CultureLanguagesForTest))]
        public async Task Error_Validation(string cultureLanguage)
        {
            var request = RegisterProductRequestBuilder.Create();
            request.ProductName = string.Empty;

            if (_httpClient.DefaultRequestHeaders.Contains("Accepted-Language"))
                _httpClient.DefaultRequestHeaders.Remove("Accepted-Language");

            _httpClient.DefaultRequestHeaders.Add("Accepted-Language", cultureLanguage);

            var response = await _httpClient.PostAsJsonAsync("products", request);

            await using var readStream = await response.Content.ReadAsStreamAsync();
            var toJsonResponse = await JsonDocument.ParseAsync(readStream);

            var errors = toJsonResponse.RootElement.GetProperty("errors").EnumerateArray();
            errors.Should().ContainSingle(ResourceExceptMessages.NAME_EMPTY);
        }
    }
}
