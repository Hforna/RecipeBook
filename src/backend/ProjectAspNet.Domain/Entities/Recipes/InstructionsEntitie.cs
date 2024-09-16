using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Entities.Recipes
{
    public class InstructionsEntitie
    {
        public int Step { get; set; }
        public string Text { get; set; } = string.Empty;
        public long RecipeId { get; set; }
    }
}
