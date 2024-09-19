using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Communication.Responses
{
    public class ResponseInstructions
    {
        public string Id { get; set; } = string.Empty;
        public int Step { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
