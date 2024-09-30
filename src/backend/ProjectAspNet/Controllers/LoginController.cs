using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAspNet.Application.UseCases.Repositories.Login;
using ProjectAspNet.Application.UseCases.RepositoriesUseCases.User;
using ProjectAspNet.Communication.Requests;
using ProjectAspNet.Communication.Responses;
using ProjectAspNet.Controllers.BaseController;
using System.Security.Claims;

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

        [HttpGet]
        [Route("google")]
        public async Task<IActionResult> LoginGoogle([FromServices] IGoogleLoginUseCase useCase, string returnUrl)
        {
            var result = await Request.HttpContext.AuthenticateAsync("Google");

            if(IsNotAuthenticated(result))
                return Challenge(GoogleDefaults.AuthenticationScheme);

            var claims = result.Principal!.Identities.First().Claims;
            var name = claims.First(d => d.Type == ClaimTypes.Name).Value;
            var email = claims.First(d => d.Type == ClaimTypes.Email).Value;

            await useCase.Execute(name, email);

            return Redirect(returnUrl);
        }

        protected static bool IsNotAuthenticated(AuthenticateResult result)
        {
            return result.Succeeded.Equals(false) 
                || result.Principal is null 
                || result.Principal.Identities.Any(d => d.IsAuthenticated) == false;
        }
    }
}
