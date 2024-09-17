using ProjectAspNet.Communication.Requests.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Communication.Requests
{
    public class RequestRecipe
    {
        public string Title { get; set; } = string.Empty;
        public CookingTime? TimeRecipe { get; set; }
        public Difficulty? Difficulty { get; set; }
        public IList<RequestInstructions> Instructions { get; set; } = [];
        public IList<string> Ingredients { get; set; } = [];
        public IList<DishType> DishType { get; set; } = [];
    }
}
