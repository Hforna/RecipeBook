using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAspNet.Application.UseCases.RepositoriesUseCases.User;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Communication.Responses;
using ProjectAspNet.Controllers.BaseController;

namespace ProjectAspNet.Controllers
{
    public class LoginController : BaseControllerProject
    {
        [HttpPost]
        [ProducesResponseType(typeof(RegisterUserResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request, [FromServices] ILoginUser useCase)
        {
            var response = await useCase.Execute(request);

            return Ok(response);
        }
    }
}
