using ProjectAspNet.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.DTOs
{
    public record GenerateRecipeDto
    {
        public string Title { get; set; } = string.Empty;
        public CookingTime CookingTime { get; set; }
        public IList<string> Ingredients { get; set; } = [];
        public IList<InstructionsRecipeDto> Instructions { get; set; } = [];
    }
}
