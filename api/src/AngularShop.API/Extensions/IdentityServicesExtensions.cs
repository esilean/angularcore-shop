using System;
using System.Text;
using AngularShop.Core.Entities.Identity;
using AngularShop.Core.Settings;
using AngularShop.Infra.Identity;
using AngularShop.Infra.Requirements;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AngularShop.API.Extensions
{
    public static class IdentityServicesExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, ApiSettingsData apiSettingsData)
        {
            var builder = services.AddIdentityCore<AppUser>();
            builder = new IdentityBuilder(builder.UserType, builder.Services);
            builder.AddEntityFrameworkStores<AppIdentityDbContext>();
            builder.AddSignInManager<SignInManager<AppUser>>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsMasterEmail", policy =>
                {
                    policy.Requirements.Add(new IsMasterEmailRequirement());
                });
            });
            services.AddTransient<IAuthorizationHandler, IsMasterEmailRequirementHandler>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiSettingsData.Token.Key)),
                        ValidIssuer = apiSettingsData.Token.Issuer,
                        ValidAudience = apiSettingsData.Token.Audience,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });


            return services;
        }
    }
}