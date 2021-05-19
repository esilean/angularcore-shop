using System.Linq;
using System.Security.Claims;
using AngularShop.Application.Services.Accessors;
using Microsoft.AspNetCore.Http;

namespace AngularShop.Infra.Services.Accessors
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public ClaimsPrincipal GetUser()
        {
            return _httpContextAccessor.HttpContext.User;
            //return _httpContextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.Email);
            //return _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        }
    }
}