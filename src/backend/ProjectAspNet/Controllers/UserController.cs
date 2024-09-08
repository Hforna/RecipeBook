using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAspNet.Application.UseCases.User;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Communication.Responses;
using ProjectAspNet.Controllers.BaseController;

namespace ProjectAspNet.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : BaseControllerProject
    {
        [HttpPost]
        [ProducesResponseType(typeof(RegisterUserResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request, [FromServices] IUserCase userCase)
        {
            var result = await userCase.Execute(request);

            return Created(string.Empty, result);
        }
    }
}
