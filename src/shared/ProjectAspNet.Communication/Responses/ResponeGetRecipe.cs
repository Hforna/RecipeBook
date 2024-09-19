using ProjectAspNet.Communication.Requests.Enums;
using ProjectAspNet.Domain.Entities.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Communication.Responses
{
    public class ResponeGetRecipe
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public CookingTime? TimeRecipe { get; set; }
        public Difficulty? Difficulty { get; set; }
        public IList<ResponseInstructions> Instructions { get; set; } = [];
        public IList<DishType> DishTypes { get; set; } = [];
        public IList<IngredientsResponse> Ingredients { get; set; } = [];
    }
}
