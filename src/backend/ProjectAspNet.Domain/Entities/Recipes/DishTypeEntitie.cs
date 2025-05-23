﻿using ProjectAspNet.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Entities.Recipes
{
    [Table("dishtype")]
    public class DishTypeEntitie : BaseEntitie
    {
        [MaxLength(1, ErrorMessage = "Invalid dish type")]
        public DishType Type { get; set; }
        public long RecipeId { get; set; }
    }
}
