using ProjectAspNet.Domain.Repositories.Security.Tokens;
using ProjectAspNet.Exceptions.Exceptions;

namespace ProjectAspNet.Token
{
    public class TokenRecepetor : ITokenReceptor
    {
        public IHttpContextAccessor _httpContext;

        public TokenRecepetor(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public string Value()
        {
            var token = _httpContext.HttpContext!.Request.Headers.Authorization.ToString();
            if(string.IsNullOrEmpty(token))
                throw new ProjectExceptionBase(ResourceExceptMessages.NO_TOKEN);
            return token["Bearer ".Length..].Trim();
        }
    }
}
