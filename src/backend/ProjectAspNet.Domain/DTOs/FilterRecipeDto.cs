using ProjectAspNet.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.DTOs
{
    public record FilterRecipeDto
    {
        public string? TitleIngredientsRecipe {  get; init; }
        public IList<DishType> DishTypes { get; init; } = [];
        public IList<CookingTime> CookingTime { get; init; } = [];
        public IList<Difficulty> Difficulty { get; init; } = [];
    }
}
