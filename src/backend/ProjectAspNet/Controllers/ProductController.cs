using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAspNet.Application.UseCases.RepositoriesUseCases.Product;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Controllers.BaseController;

namespace ProjectAspNet.Controllers
{
    public class ProductController : BaseControllerProject
    {
        [HttpPost]
        public async Task<IActionResult> Execute([FromBody] RegisterProductRequest request, [FromServices] IProductCase productCase)
        {
            var result = await productCase.Register(request);

            return Created(string.Empty, result);
        }
    }
}
