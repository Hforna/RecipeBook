using Moq;
using ProjectAspNet.Application.Services.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Cryptography
{
    public class CryptographyBuild
    {
        public static PasswordCryptography Build()
        {
            return new PasswordCryptography("ljrbw24=68j/sns");
        }
    }
}
