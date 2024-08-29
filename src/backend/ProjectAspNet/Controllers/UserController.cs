using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAspNet.Application.UseCases.User;
using ProjectAspNet.Communication.Requests;

namespace ProjectAspNet.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request, [FromServices] IUserCase userCase)
        {
            var result = await userCase.Execute(request);

            return Created(string.Empty, result);
        }
    }
}
