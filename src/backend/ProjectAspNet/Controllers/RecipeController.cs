using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAspNet.Application.UseCases.Repositories.Recipe;
using ProjectAspNet.Attributes;
using ProjectAspNet.Binders;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Communication.Responses;
using ProjectAspNet.Controllers.BaseController;
using ProjectAspNet.Exceptions.Exceptions;

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

        [HttpPost("filters")]
        [ProducesResponseType(typeof(GetRecipesResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Filter([FromBody] RequestFilterRecipe request, [FromServices] IFilterRecipeUseCase useCase)
        {
            var response = await useCase.Execute(request);

            return Ok(response);
        }

        [HttpGet]
        [Route("{Id}")]
        [ProducesResponseType(typeof(ResponeGetRecipe), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetRecipeException), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromRoute][ModelBinder(typeof(RecipeIdBinder))] long Id, [FromServices] IGetRecipeUseCase useCase)
        {
            var result = await useCase.Execute(Id);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{Id}")]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(GetRecipeException), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute][ModelBinder(typeof(RecipeIdBinder))] long Id, [FromServices] IDeleteRecipe useCase)
        {
            await useCase.Execute(Id);

            return NoContent();
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetRecipeException), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute][ModelBinder(typeof(RecipeIdBinder))] long id, [FromServices] IUpdateRecipeUseCase useCase, [FromBody] RequestRecipe request)
        {
            await useCase.Execute(id, request);

            return NoContent();
        }

        [HttpGet("dashboard")]
        [ProducesResponseType(typeof(GetRecipesResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Dashboard([FromServices] IRecipeDashboardUseCase useCase)
        {
            var result = await useCase.Execute();

            if(result.Recipe.Any() == false)
                return NoContent();

            return Ok(result);
        }

        [HttpPost("generate")]
        [ProducesResponseType(typeof(ResponseGenerateRecipe), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RecipeGenerateException), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Generate([FromBody] RequestGenerateRecipe request, [FromServices] IGenerateRecipeUseCase useCase)
        {
            var result = await useCase.Execute(request);

            return Ok(result);
        }
    }
}
