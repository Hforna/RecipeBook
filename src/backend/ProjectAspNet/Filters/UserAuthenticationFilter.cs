using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using ProjectAspNet.Domain.Repositories.Security.Tokens;
using ProjectAspNet.Domain.Repositories.Users;
using ProjectAspNet.Exceptions.Exceptions;

namespace ProjectAspNet.Filters
{
    public class UserAuthenticationFilter : IAsyncAuthorizationFilter
    {
        private readonly ITokenValidator _tokenValidator;
        private readonly IUserIdentifierExists _userIdentifierExists;

        public UserAuthenticationFilter(ITokenValidator tokenValidator, IUserIdentifierExists userIdentifierExists)
        {
            _tokenValidator = tokenValidator;
            _userIdentifierExists = userIdentifierExists;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var token = getToken(context);
                var validator = _tokenValidator.Validate(token);

                var userExists = _userIdentifierExists.UserIdentifierExists(validator);
                if (await userExists == false)
                    throw new RegisterUserError(ResourceExceptMessages.USER_IDENTIFIER_NOT_EXISTS);
            }
            catch (SecurityTokenExpiredException ex)
            {
                context.Result = new UnauthorizedObjectResult(new RegisterUserError(ex.Message));
            }
            catch (ProjectExceptionBase ex)
            {
                context.Result = new UnauthorizedObjectResult(new ProjectExceptionBase(ex.Message));
            }
            catch
            {
                context.Result = new UnauthorizedObjectResult(new RegisterUserError(ResourceExceptMessages.USER_DOESNT_EXISTS));
            }
        }

        private string getToken(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Response.Headers.Authorization.ToString();
            if (string.IsNullOrEmpty(token))
                throw new ProjectExceptionBase(ResourceExceptMessages.NAME_EMPTY);
            return token["Bearer ".Length..].Trim();
        }
    }
}
