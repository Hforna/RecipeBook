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
    public class ValidateChangePasswordTest
    {
        [Fact]
        public void Success()
        {
            var request = ChangePasswordRequestBuilder.Build();
            var validate = new ChangePasswordValidate();
            var result = validate.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_Password_Length()
        {
            var request = ChangePasswordRequestBuilder.Build(6);
            var validate = new ChangePasswordValidate();
            var result = validate.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(ResourceExceptMessages.PASSWORD_LESS);
        }
    }
}
