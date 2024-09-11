using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using CommonTestUtilities.Cryptography;
using ProjectAspNet.Application.Services.Cryptography;
using ProjectAspNet.Domain.Entities;

namespace CommonTestUtilities.Entities
{
    public class UserEntitieTest
    {
        public static (UserEntitie user, string password) Build()
        {
            var password = new Faker().Internet.Password();
            var user =  new Faker<UserEntitie>()
                .RuleFor(x => x.Id, (f) => 1)
                .RuleFor(x => x.Name, (f) => f.Person.FirstName)
                .RuleFor(x => x.Email, (f) => f.Internet.Email())
                .RuleFor(x => x.Password, (f) => CryptographyBuild.Build().Encrypt(password));

            return (user, password);
        }
    }
}
