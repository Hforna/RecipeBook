using FluentValidation;
using FluentValidation.Validators;
using ProjectAspNet.Exceptions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.SharedValidators
{
    public class PasswordValidator<T> : PropertyValidator<T, string>
    {
        public override string Name => "PasswordValidator";

        public override bool IsValid(ValidationContext<T> context, string password)
        {
            if(string.IsNullOrEmpty(password))
            {
                context.MessageFormatter.AppendArgument("ErrorMessage", ResourceExceptMessages.PASSWORD_EMPTY);

                return false;
            }

            if(password.Length < 8 && string.IsNullOrEmpty(password) == false)
            {
                context.MessageFormatter.AppendArgument("ErrorMessage", ResourceExceptMessages.PASSWORD_LESS);

                return false;
            }

            return true;
        }

        protected override string GetDefaultMessageTemplate(string errorCode) => "{ErrorMessage}";
    }
}
