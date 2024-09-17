using CommonTestUtilities.Request.User;
using FluentAssertions;
using ProjectAspNet.Application.UseCases.User;
using ProjectAspNet.Application.Validators.User;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators
{
    public class UpdateUserValidateTest
    {
        [Fact]
        public void Success()
        {
            var request = UpdateUserRequestBuilder.Build();
            var validate = new UpdateUserValidate();
            var result = validate.Validate(request);
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_Email_Format()
        {
            var request = UpdateUserRequestBuilder.Build();
            request.Email = "asd.com";
            var validate = new UpdateUserValidate();
            var result = validate.Validate(request);
            result.Errors.Should().ContainSingle(ResourceExceptMessages.EMAIL_FORMAT);
        }
    }
}
