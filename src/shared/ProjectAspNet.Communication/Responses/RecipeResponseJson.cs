using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Communication.Responses
{
    public class RecipeResponseJson
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public int AmountIngredients { get; set; }
    }
}
