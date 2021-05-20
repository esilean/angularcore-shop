using System.Security.Claims;
using System.Threading.Tasks;
using AngularShop.Core.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace AngularShop.Infra.Requirements
{
    public class IsMasterEmailRequirement : IAuthorizationRequirement
    {
    }

    public class IsMasterEmailRequirementHandler : AuthorizationHandler<IsMasterEmailRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IsMasterEmailRequirementHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsMasterEmailRequirement requirement)
        {
            var emailCurrentUser = _httpContextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrWhiteSpace(emailCurrentUser))
                throw new SomeException(400);

            if (emailCurrentUser == "le.bevilaqua@gmail.com")
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}