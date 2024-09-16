using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Communication.Requests
{
    public class RequestInstructions
    {
        public int Step {  get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
