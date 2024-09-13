using Bogus;
using ProjectAspNet.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Request.User
{
    public class UpdateUserRequestBuilder
    {
        public static RequestUpdateUser Build()
        {
            return new Faker<RequestUpdateUser>()
                .RuleFor(u => u.Name, (f) => f.Name.FirstName())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name));
        }
    }
}
