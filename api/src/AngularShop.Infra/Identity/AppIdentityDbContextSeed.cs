using System.Linq;
using System.Threading.Tasks;
using AngularShop.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace AngularShop.Infra.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Bevila",
                    Email = "le.bevilaqua@gmail.com",
                    UserName = "le.bevilaqua@gmail.com",
                    Address = new Address
                    {
                        FirstName = "Bevila",
                        LastName = "Bevilaty",
                        Street = "10 The Street",
                        City = "New York",
                        State = "NY",
                        ZipCode = "90999"
                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}