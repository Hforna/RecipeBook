using CommonTestUtilities.Request.User;
using FluentAssertions;
using ProjectAspNet.Application.UseCases.User;
using ProjectAspNet.Application.Validators.User;
using ProjectAspNet.Exceptions.Exceptions;
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
            var validate = new UserRegisterValidate();
            var result = validate.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_Email_Format()
        {
            var request = RegisterUserRequestBuilder.Create();
            request.Email = "asd.com";
            var validate = new UserRegisterValidate();
            var result = validate.Validate(request);

            result.Errors.Should().ContainSingle(ResourceExceptMessages.EMAIL_FORMAT);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void Password_Length(int passwordLength)
        {
            var request = RegisterUserRequestBuilder.Create(passwordLength);
            var validate = new UserRegisterValidate();
            var result = validate.Validate(request);

            result.Errors.Should().Contain(e => e.ErrorMessage.Equals(ResourceExceptMessages.PASSWORD_LESS) || e.ErrorMessage.Equals(ResourceExceptMessages.NAME_EMPTY));
        }
    }
}
