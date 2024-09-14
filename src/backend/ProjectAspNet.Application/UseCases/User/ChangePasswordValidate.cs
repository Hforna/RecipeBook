using FluentValidation;
using ProjectAspNet.Application.SharedValidators;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.UseCases.User
{
    public class ChangePasswordValidate : AbstractValidator<RequestChangeUserPassword>
    {
        public ChangePasswordValidate()
        {
            RuleFor(x => x.NewPassword).SetValidator(new PasswordValidator<RequestChangeUserPassword>());
        }
    }
}
