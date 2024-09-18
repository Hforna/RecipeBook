using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Communication.Responses
{
    public class GetRecipesResponse
    {
        public IList<RecipeResponseJson> Recipe { get; set; } = [];
    }
}
