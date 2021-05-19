using System.Security.Claims;

namespace AngularShop.Application.Services.Accessors
{
    public interface IUserAccessor
    {
        ClaimsPrincipal GetUser();
    }
}