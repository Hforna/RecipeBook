using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Exceptions.Exceptions
{
    public class ResponseErrorJson : SystemException
    {
        public IList<string> Errors { get; set; } = new List<string>();

        public ResponseErrorJson(IList<string> errors) => Errors = errors;

        public ResponseErrorJson(string message) => Errors.Add(message);
    }
}
