using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAspNet.Application.UseCases.Repositories.Recipe;
using ProjectAspNet.Attributes;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Communication.Responses;
using ProjectAspNet.Controllers.BaseController;

namespace ProjectAspNet.Controllers
{
    [UserAuthentication]
    public class RecipeController : BaseControllerProject
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRecipe), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] RequestRecipe request, ICreateRecipe useCase)
        {
            var result = await useCase.Execute(request);

            return Ok(result);
        }
    }
}
