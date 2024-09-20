using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Repositories.Recipes
{
    public interface IDeleteRecipeById
    {
        public Task DeleteById(long id);
    }
}
