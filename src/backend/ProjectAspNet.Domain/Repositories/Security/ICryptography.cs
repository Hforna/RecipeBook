using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Repositories.Security
{
    public interface ICryptography
    {
        public string Encrypt(string password);

        public bool IsValid(string password, string hashPassword);
    }
}
