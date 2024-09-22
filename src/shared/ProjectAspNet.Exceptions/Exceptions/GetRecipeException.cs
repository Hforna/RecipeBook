using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Exceptions.Exceptions
{
    public class GetRecipeException : ProjectExceptionBase
    {
        public IList<string> Errors { get; set; } = new List<string>();
        public GetRecipeException(string message) : base(message) => Errors.Add(message);

        public GetRecipeException(IList<string> errors) : base(string.Empty)
        {
            Errors = errors;
        }

        public override IList<string> GetErrorMessages() => Errors;

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
    }
}
