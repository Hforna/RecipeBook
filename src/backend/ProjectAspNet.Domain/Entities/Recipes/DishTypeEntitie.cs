using ProjectAspNet.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Entities.Recipes
{
    public class DishTypeEntitie
    {
        public DishType Type { get; set; }
        public long RecipeId { get; set; }
    }
}
