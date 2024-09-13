using FluentValidation;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.UseCases.User
{
    public class UpdateUserValidate : AbstractValidator<RequestUpdateUser>
    {
        public UpdateUserValidate()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ResourceExceptMessages.NAME_EMPTY);
            When(x => string.IsNullOrEmpty(x.Email) == false, () =>
            {
                RuleFor(x => x.Email).EmailAddress().WithMessage(ResourceExceptMessages.EMAIL_FORMAT);
            });
        }
    }
}
