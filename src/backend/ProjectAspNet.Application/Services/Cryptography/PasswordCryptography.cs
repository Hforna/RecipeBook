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
        private readonly string _keyWord = "asd";
        public PasswordCryptography(string keyWord) => _keyWord = keyWord;

        public PasswordCryptography()
        {
        }

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
