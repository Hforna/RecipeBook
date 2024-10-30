using ProjectAspNet.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Entities.Recipes
{
    [Table("recipes")]
    public class Recipe : BaseEntitie
    {
        [MaxLength(255, ErrorMessage = "Field must have only 255 digits or less")]
        public string? Title { get; set; }
        [MaxLength(1, ErrorMessage = "Invalid time recipe type")]
        public CookingTime? TimeRecipe { get; set; }
        [MaxLength(1, ErrorMessage = "Invalid difficulty type")]
        public Difficulty? Difficulty { get; set; }
        public IList<InstructionsEntitie> Instructions { get; set; } = [];
        public IList<DishTypeEntitie> DishTypes { get; set; } = [];
        public IList<IngredientEntitie> Ingredients { get; set; } = [];
        public string? ImageIdentifier { get; set; }
        [ForeignKey("users")]
        public long UserId { get; set; }
    }
}
