using System.Security.Claims;
using System.Threading.Tasks;
using AngularShop.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AngularShop.Application.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindByEmailUserFromClaimsPrincipleAsync(this UserManager<AppUser> input, ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            return await input.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public static async Task<AppUser> FindByEmailUserWithAddressFromClaimsPrincipleAsync(this UserManager<AppUser> input, ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            return await input.Users.Include(x => x.Address).FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}