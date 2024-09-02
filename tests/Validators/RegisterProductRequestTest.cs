using CommonTestUtilities.Request.Product;
using FluentAssertions;
using ProjectAspNet.Application.UseCases.Product;
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
            var request = RegisterProductRequestBuilder.Create();
            var validate = new RegisterProductValidate();
            var result = validate.Validate(request);

            result.IsValid.Should().BeTrue();
        }
    }
}
