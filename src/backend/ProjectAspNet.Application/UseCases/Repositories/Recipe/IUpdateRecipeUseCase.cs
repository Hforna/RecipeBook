using ProjectAspNet.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.UseCases.Repositories.Recipe
{
    public interface IUpdateRecipeUseCase
    {
        public Task Execute(long id, RequestRecipe request);
    }
}
