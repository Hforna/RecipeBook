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
    public class UserRegisterValidate : AbstractValidator<RegisterUserRequest>
    {
        public UserRegisterValidate()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ResourceExceptMessages.NAME_EMPTY);
            RuleFor(x => x.Email).EmailAddress().WithMessage(ResourceExceptMessages.EMAIL_FORMAT);
            RuleFor(x => x.Password.Length).GreaterThanOrEqualTo(8).WithMessage(ResourceExceptMessages.PASSWORD_LESS);
        }
    }
}
