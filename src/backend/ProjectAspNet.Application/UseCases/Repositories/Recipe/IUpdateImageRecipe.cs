using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.UseCases.Repositories.Recipe
{
    public interface IUpdateImageRecipe
    {
        public Task Execute(IFormFile file, long id);
    }
}
