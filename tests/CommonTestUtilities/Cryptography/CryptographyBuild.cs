using Moq;
using ProjectAspNet.Domain.Repositories.Security;
using ProjectAspNet.Infrastructure.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Cryptography
{
    public class CryptographyBuild
    {
        public static ICryptography Build()
        {
            return new BCrypt("ljrbw24=68j/sns");
        }
    }
}
