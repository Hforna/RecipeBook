using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Exceptions.Exceptions
{
    public class RegisterUserError : ProjectExceptionBase
    {
        public List<string> Errors { get; set; }

        public RegisterUserError(List<string> errors)
        {
            Errors = errors;
        }

        public RegisterUserError(string error)
        {
            Errors = new List<string>();
            Errors.Add(error);
        }
    }
}
