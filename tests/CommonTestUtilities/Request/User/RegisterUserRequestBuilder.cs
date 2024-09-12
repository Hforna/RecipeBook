using Bogus;
using ProjectAspNet.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Request.User
{
    public class RegisterUserRequestBuilder
    {
        public static RegisterUserRequest Create(int passwordLength = 10)
        {
            return new Faker<RegisterUserRequest>()
                .RuleFor(user => user.Name, (f) => f.Name.FindName())
                .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
                .RuleFor(user => user.Password, (f) => f.Internet.Password(passwordLength));
        }
    }
}
