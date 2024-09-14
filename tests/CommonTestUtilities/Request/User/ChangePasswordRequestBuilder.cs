using Bogus;
using ProjectAspNet.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Request.User
{
    public class ChangePasswordRequestBuilder
    {
        public static RequestChangeUserPassword Build(int passwordLength = 10)
        {
            return new Faker<RequestChangeUserPassword>()
                .RuleFor(x => x.NewPassword, (f) => f.Internet.Password(passwordLength))
                .RuleFor(x => x.Password, (f) => f.Internet.Password(passwordLength));
        }
    }
}
