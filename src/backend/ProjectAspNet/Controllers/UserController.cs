using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProjectAspNet.Application.UseCases.Repositories.User;
using ProjectAspNet.Application.UseCases.RepositoriesUseCases.User;
using ProjectAspNet.Attributes;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Communication.Responses;
using ProjectAspNet.Controllers.BaseController;

namespace ProjectAspNet.Controllers
{
    public class UserController : BaseControllerProject
    {
        [HttpPost]
        [ProducesResponseType(typeof(RegisterUserResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request, [FromServices] IUserCase userCase)
        {
            var result = await userCase.Execute(request);

            return Created(string.Empty, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseUserProfile), StatusCodes.Status200OK)]
        [UserAuthentication]
        public async Task<IActionResult> GetProfile([FromServices] IGetProfileUseCase userCase)
        {
            var result = await userCase.Execute();

            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [UserAuthentication]
        public async Task<IActionResult> Update([FromServices] IUpdateUserUseCase useCase, [FromBody] RequestUpdateUser request)
        {
            await useCase.Execute(request);

            return NoContent();
        }

        [HttpPatch("change-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [UserAuthentication]
        public async Task<IActionResult> ChangePassword([FromBody] RequestChangeUserPassword request, [FromServices] IChangePasswordUseCase useCase)
        {
            await useCase.Execute(request);
            return NoContent();
        }

        [HttpDelete]
        [UserAuthentication]
        public async Task<IActionResult> DeleteAccount([FromServices] IRequesteDeleteUserUseCase useCase)
        {
            await useCase.Execute();

            return NoContent();
        }
    }
}
