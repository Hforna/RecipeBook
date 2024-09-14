using ProjectAspNet.Domain.Repositories.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Infrastructure.Security.Cryptography
{
    public class Sha512Encrypt : ICryptography
    {
        private readonly string _keyWord = "asd";
        public Sha512Encrypt(string keyWord) => _keyWord = keyWord;

        public string Encrypt(string password)
        {
            var newPassword = $"{_keyWord}{password}";
            var bytes = Encoding.UTF8.GetBytes(newPassword);
            var encrypt = SHA512.HashData(bytes);
            return ToString(encrypt);
        }

        public static string ToString(byte[] bytes)
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
