using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using ProjectAspNet.Communication.Requests;
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
    public class UpdateUserTest : IClassFixture<DataBaseInMemoryApi>
    {
        private readonly HttpClient _httpClient;
        private readonly DataBaseInMemoryApi _factory;
        public UpdateUserTest(DataBaseInMemoryApi factory)
        {
            _factory = factory;
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task Success()
        {
            var generate = JwtTokenGenerate.Build();
            var user = UserEntitieTest.Build();
            var request = new RequestUpdateUser() { Name = user.user.Name, Email = user.user.Email };
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", generate.Generate(_factory.getUserIdentifier()));
            var response = await _httpClient.PutAsJsonAsync("user", request);
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Error_Email_Exists()
        {
            var generate = JwtTokenGenerate.Build();
            var user = UserEntitieTest.Build();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", generate.Generate(_factory.getUserIdentifier()));
            var request = new RequestUpdateUser() { Name = user.user.Name, Email = _factory.getEmail() };
            var response = await _httpClient.PutAsJsonAsync("user", request);
            var readAsStream = response.Content.ReadAsStream();
            var responseContent = JsonDocument.Parse(readAsStream);
            var listErrors = responseContent.RootElement.GetProperty("errors").EnumerateArray();
            var errors = listErrors.Select(e => e.GetString()).ToList();
            errors.Should().Contain(ResourceExceptMessages.EMAIL_ALREADY_EXISTS);
        }
    }
}
