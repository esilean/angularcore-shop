using AngularShop.API.Extensions;
using AngularShop.API.Middleware;
using AngularShop.Application.Dtos.Login;
using AngularShop.Application.Mappers;
using AngularShop.Application.Services.Accessors;
using AngularShop.Application.Services.Security;
using AngularShop.Core.Settings;
using AngularShop.Infra.Data;
using AngularShop.Infra.Identity;
using AngularShop.Infra.Services.Accessors;
using AngularShop.Infra.Services.Security;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using StackExchange.Redis;

namespace AngularShop.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var apiSettingsData = new ApiSettingsData();
            _configuration.Bind(apiSettingsData);
            services.AddSingleton(apiSettingsData);

            var cnnString = _configuration.GetConnectionString("DefaultCnn");
            services.AddDbContext<StoreContext>(x =>
            {
                x.UseMySql(cnnString, ServerVersion.AutoDetect(cnnString));
            });
            services.AddDbContext<AppIdentityDbContext>(x =>
            {
                x.UseMySql(cnnString, ServerVersion.AutoDetect(cnnString));
            });

            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var configuration = new ConfigurationOptions
                {
                    EndPoints = { _configuration.GetConnectionString("Redis"), "6379" },
                    AbortOnConnectFail = false,
                    ConnectRetry = 2
                };
                return ConnectionMultiplexer.Connect(configuration);
            });

            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddControllers(opts =>
            {
                // var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                // opts.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddFluentValidation(cfg =>
            {
                cfg.RegisterValidatorsFromAssemblyContaining<LoginRequest>();
            });
            services.AddHttpContextAccessor();

            services.AddScoped<ICorrelationIdAccessor, CorrelationIdAccessor>();
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddIdentityServices(apiSettingsData);
            services.AddApplicationServices();
            services.AddSwaggerDoc();

            services.AddCors(opts =>
            {
                opts.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader()
                            .AllowAnyMethod()
                            .WithOrigins(apiSettingsData.ApiOriginUrl);
                });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Log.Information($"Hosting enviroment = {env.EnvironmentName}");

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<LogHeaderMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseSwaggerDoc();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();

            app.UseRouting();
            app.UseStaticFiles();

            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
