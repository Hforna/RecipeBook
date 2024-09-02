using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Exceptions.Exceptions
{
    public class RegisterProductError : ProjectExceptionBase
    {
        public IList? Errors { get; set; }

        public RegisterProductError(IList errors)
        {
            Errors = errors;
        }

        public RegisterProductError(string error)
        {
            Errors = new List<RegisterProductError>();
            Errors.Add(error);
        }
    }
}
