using ProjectAspNet.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Repositories.OpenAi
{
    public interface IGenerateRecipeAi
    {
        public Task<GenerateRecipeDto> Generate(IList<string> recipes);
    }
}
