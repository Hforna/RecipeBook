using ProjectAspNet.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Entities.Recipes
{
    public class RecipeEntitie : BaseEntitie
    {
        public string? Title { get; set; }
        public CookingTime TimeRecipe { get; set; }
        public Difficulty Difficulty { get; set; }
        public IList<InstructionsEntitie> Instructions { get; set; } = new List<InstructionsEntitie>();
        public IList<DishTypeEntitie> DishTypes { get; set; } = [];
        public string Ingredients { get; set; } = string.Empty;
        public long UserId { get; set; }
    }
}
