using FluentAssertions;
using ProjectAspNet.Communication.Requests;
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
    public class LoginIntegrationTest : IClassFixture<DataBaseInMemoryApi>
    {
        private readonly HttpClient _httpClient;
        private readonly string _username;
        private readonly string _password;
        private readonly string _email;

        public LoginIntegrationTest(DataBaseInMemoryApi factory)
        {
            _httpClient = factory.CreateClient();
            _email = factory.getEmail();
            _password = factory.getPassword();
            _username = factory.getUsername();
        }

        [Fact]
        public async Task Success()
        {
            var request = new LoginUserRequest() { Email = _email, Password = _password};
            var response = await _httpClient.PostAsJsonAsync("login", request);
            await using var readAsStream = await response.Content.ReadAsStreamAsync();
            var contextResponse = JsonDocument.ParseAsync(readAsStream);
            contextResponse.Result.RootElement.GetProperty("name").Should().Be(_username);
        }
    }
}
