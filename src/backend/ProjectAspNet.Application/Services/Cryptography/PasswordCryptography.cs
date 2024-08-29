using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.Services.Cryptography
{
    public class PasswordCryptography
    {
        public string Encrypt(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var encrypt = SHA512.HashData(bytes);
            return ToString(encrypt);
        }

        public string ToString(byte[] bytes)
        {
            var sB = new StringBuilder();
            foreach (byte b in bytes)
            {
                var cb = b.ToString("x2");
                sB.Append(cb);
            }
            return sB.ToString();
        }
    }
}
