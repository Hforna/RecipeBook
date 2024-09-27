using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Communication.Requests
{
    public class RequestRecipeImage : RequestRecipe
    {
        public IFormFile? Image { get; set; }
    }
}
