using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using ProjectAspNet.Domain.Repositories.Security.Tokens;
using ProjectAspNet.Domain.Repositories.Users;
using ProjectAspNet.Exceptions.Exceptions;
using ProjectAspNet.Filters;

namespace ProjectAspNet.Attributes
{
    public class UserAuthenticationAttribute : TypeFilterAttribute
    {
        public UserAuthenticationAttribute() : base(typeof(UserAuthenticationFilter))
        {
        }
    }
}
