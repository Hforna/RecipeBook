using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Entities.Recipes
{
    [Table("instructions")]
    public class InstructionsEntitie : BaseEntitie
    {
        public int Step { get; set; }
        [Required]
        [MaxLength(255, ErrorMessage = "Text can't have more than 400 letters")]
        public string Text { get; set; } = string.Empty;
        [ForeignKey("recipes")]
        public long RecipeId { get; set; }
    }
}
