using CommonTestUtilities.Request.User;
using FluentAssertions;
using ProjectAspNet.Application.UseCases.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators
{
    public class RegisterUserRequestTest
    {
        [Fact]
        public void Success()
        {
            var request = RegisterUserRequestBuilder.Create();
            var validator = new UserRegisterValidate();
            request.Name = string.Empty;
            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
        }
    }
}
