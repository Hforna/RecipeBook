using CommonTestUtilities.Request.Product;
using FluentAssertions;
using ProjectAspNet.Application.UseCases.Product;
using ProjectAspNet.Application.Validators.Product;
using ProjectAspNet.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators
{
    public class RegisterProductRequestTest
    {
        [Fact]
        public void Success()
        {
            var request = RegisterProductRequestBuilder.Create(1);
            var validate = new RegisterProductValidate();
            var result = validate.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_Validation()
        {
            var request = RegisterProductRequestBuilder.Create(100001);
            var validate = new RegisterProductValidate();
            var result = validate.Validate(request);

            result.Errors.Should().ContainSingle(ResourceExceptMessages.HIGH_PRICE);
        }
    }
}
