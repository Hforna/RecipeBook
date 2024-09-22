using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using ProjectAspNet.Domain.Repositories.Security.Tokens;
using ProjectAspNet.Domain.Repositories.Users;
using ProjectAspNet.Exceptions.Exceptions;
using ProjectAspNet.Token;

namespace ProjectAspNet.Filters
{
    public class UserAuthenticationFilter : IAsyncAuthorizationFilter
    {
        private readonly ITokenValidator _tokenValidator;
        private readonly IUserIdentifierExists _userIdentifierExists;
        private readonly ITokenReceptor _tokenReceptor;

        public UserAuthenticationFilter(ITokenValidator tokenValidator, IUserIdentifierExists userIdentifierExists, ITokenReceptor tokenReceptor)
        {
            _tokenReceptor = tokenReceptor;
            _tokenValidator = tokenValidator;
            _userIdentifierExists = userIdentifierExists;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var token = _tokenReceptor.Value();
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
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ex.Message));
            }
            catch
            {
                context.Result = new UnauthorizedObjectResult(new RegisterUserError(ResourceExceptMessages.USER_DOESNT_EXISTS));
            }
        }
    }
}
