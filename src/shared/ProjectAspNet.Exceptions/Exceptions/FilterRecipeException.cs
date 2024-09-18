using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Exceptions.Exceptions
{
    public class FilterRecipeException : ProjectExceptionBase
    {
        public List<string> Errors { get; set; } = new List<string>();

        public FilterRecipeException(string message) : base(message)
        {
            Errors.Add(message);
        }

        public FilterRecipeException(List<string> errors) : base(string.Empty)
        {
            Errors = errors;
        }
    }
}
