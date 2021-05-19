using AngularShop.Core.Entities.Identity;

namespace AngularShop.Application.Services.Security
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}