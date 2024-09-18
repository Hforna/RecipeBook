using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Entities.Recipes
{
    [Table("ingredients")]
    public class IngredientEntitie : BaseEntitie
    {
        public string Item { get; set; } = string.Empty;
        public long RecipeId { get; set; }
    }
}
