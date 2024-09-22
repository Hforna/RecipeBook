using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Exceptions.Exceptions
{
    public class LoginUserException : ProjectExceptionBase
    {
        public LoginUserException() : base(ResourceExceptMessages.EMAIL_OR_PASSWORD_INVALID) {}

        public override IList<string> GetErrorMessages()
        {
            throw new NotImplementedException();
        }

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}
