using ProjectAspNet.Communication.Requests.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Communication.Requests
{
    public class RequestFilterRecipe
    {
        public string? TitleIngredientsRecipe { get; set; }
        public IList<DishType> DishTypes { get; set; } = [];
        public IList<Difficulty> Difficulty { get; set; } = [];
        public IList<CookingTime> CookingTime { get; set; } = [];
    }
}
