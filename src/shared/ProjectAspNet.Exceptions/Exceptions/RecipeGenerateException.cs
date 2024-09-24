using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Exceptions.Exceptions
{
    public class RecipeGenerateException : ProjectExceptionBase
    {
        public IList<string>? Errors = [];

        public RecipeGenerateException(string message) : base(message) => Errors.Add(message);

        public RecipeGenerateException(IList<string>? errors) : base(string.Empty) => Errors = errors;
        public override IList<string> GetErrorMessages() => Errors!;

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
    }
}
