using FluentValidation;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.UseCases.Product
{
    public class RegisterProductValidate : AbstractValidator<RegisterProductRequest>
    {
        public RegisterProductValidate()
        {
            RuleFor(pr => pr.ProductName).NotEmpty().WithMessage(ResourceExceptMessages.NAME_EMPTY);
            RuleFor(pr => pr.Price).LessThanOrEqualTo(1000000).WithMessage(ResourceExceptMessages.HIGH_PRICE);
            RuleFor(pr => pr.Quantity).NotNull();
            When(pr => string.IsNullOrEmpty(pr.ProductName) == false, () =>
            {
                RuleFor(pr => pr.ProductName).MaximumLength(100);
            });
        }
    }
}
