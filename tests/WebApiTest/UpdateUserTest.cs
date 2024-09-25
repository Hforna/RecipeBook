﻿using CommonTestUtilities.Entities;
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
        private readonly string _getEmail;
        private readonly string _username;
        private readonly string _password;
        private readonly Guid _userIdentifier;
        public UpdateUserTest(DataBaseInMemoryApi factory)
        {
            _httpClient = factory.CreateClient();
            _getEmail = factory.getEmail();
            _username = factory.getUsername();
            _password = factory.getPassword();
            _userIdentifier = factory.getUserIdentifier();
        }

        [Fact]
        public async Task Success()
        {
            var generate = JwtTokenGenerate.Build();
            var request = new RequestUpdateUser() { Name = "asdasd", Email = "henriqueere@gmai.com" };
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", generate.Generate(_userIdentifier));
            var response = await _httpClient.PutAsJsonAsync("user", request);
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Error_Email_Exists()
        {
            var generate = JwtTokenGenerate.Build();
            var request = new RequestUpdateUser() { Name = "asdasd", Email = "henriqueere@gmai.com" };
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", generate.Generate(_userIdentifier));
            var response = await _httpClient.PutAsJsonAsync("user", request);
            var readAsStream = await response.Content.ReadAsStreamAsync();
            var responseContent = JsonDocument.Parse(readAsStream);
            var listErrors = responseContent.RootElement.GetProperty("errors").EnumerateArray();
            var errors = listErrors.Select(e => e.GetString()).ToList();
            errors.Should().Contain(ResourceExceptMessages.EMAIL_ALREADY_EXISTS);
        }
    }
}
