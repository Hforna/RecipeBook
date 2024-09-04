using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Exceptions.Exceptions
{
    public class RegisterProductError : ProjectExceptionBase
    {
        public List<string> Errors { get; set; }

        public RegisterProductError(List<string> errors)
        {
            Errors = errors;
        }

        public RegisterProductError(string error)
        {
            Errors = new List<string>();
            Errors.Add(error);
        }
    }
}
