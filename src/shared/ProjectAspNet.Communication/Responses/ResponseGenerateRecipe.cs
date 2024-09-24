using ProjectAspNet.Communication.Requests.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Communication.Responses
{
    public class ResponseGenerateRecipe
    {
        public string Title { get; set; } = string.Empty;
        public IList<string> Ingredients { get; set; } = [];
        public IList<ResponseInstructionsGeneratedJson> Instructions { get; set; } = [];
        public CookingTime CookingTime { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
