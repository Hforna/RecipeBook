using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Exceptions.Exceptions
{
    public class RefreshTokenException : ProjectExceptionBase
    {
        public IList<string> Errors { get; set; } = [];

        public RefreshTokenException(string message) : base(message) => Errors.Add(message);

        public RefreshTokenException(List<string> errors) : base(string.Empty) => Errors = errors;
        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
    }
}
