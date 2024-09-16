using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Entities.Recipes
{
    public class IngredientEntitie
    {
        public string Item { get; set; } = string.Empty;
        public long RecipeId { get; set; }
    }
}
