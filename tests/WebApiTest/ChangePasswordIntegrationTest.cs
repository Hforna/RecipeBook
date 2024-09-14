using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request.User;
using FluentAssertions;
using ProjectAspNet.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTest
{
    public class ChangePasswordIntegrationTest : IClassFixture<DataBaseInMemoryApi>
    {
        private readonly HttpClient _httpClient;
        private readonly string _username;
        private readonly string _password;
        private readonly Guid _guid;

        public ChangePasswordIntegrationTest(DataBaseInMemoryApi factory)
        {
            _httpClient = factory.CreateClient();
            _username = factory.getUsername();
            _password = factory.getPassword();
            _guid = factory.getUserIdentifier();

        }

        [Fact]
        public async Task Success()
        {
            var generate = JwtTokenGenerate.Build().Generate(_guid);
            var request = ChangePasswordRequestBuilder.Build();
            request.Password = _password;
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", generate);
            var response = await _httpClient.PutAsJsonAsync("user/change-password", request);
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
