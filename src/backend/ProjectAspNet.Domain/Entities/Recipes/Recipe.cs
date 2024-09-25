using ProjectAspNet.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Entities.Recipes
{
    [Table("recipes")]
    public class Recipe : BaseEntitie
    {
        public string? Title { get; set; }
        public CookingTime? TimeRecipe { get; set; }
        public Difficulty? Difficulty { get; set; }
        public IList<InstructionsEntitie> Instructions { get; set; } = [];
        public IList<DishTypeEntitie> DishTypes { get; set; } = [];
        public IList<IngredientEntitie> Ingredients { get; set; } = [];
        public string? ImageIdentifier { get; set; }
        public long UserId { get; set; }
    }
}
