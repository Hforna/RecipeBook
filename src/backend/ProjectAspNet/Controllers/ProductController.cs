using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAspNet.Application.UseCases.Product;
using ProjectAspNet.Communication.Requests;

namespace ProjectAspNet.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Execute([FromBody] RegisterProductRequest request, [FromServices] IProductCase productCase)
        {
            var result = await productCase.Register(request);

            return Created(string.Empty, result);
        }
    }
}
