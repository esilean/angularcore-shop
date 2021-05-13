using AngularShop.API.Extensions;
using AngularShop.API.Middleware;
using AngularShop.Application.Mappers;
using AngularShop.Core.Settings;
using AngularShop.Infra.Data;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddControllers();
            services.AddApplicationServices();
            services.AddSwaggerDoc();

            services.AddCors(opts =>
            {
                opts.AddPolicy("CorsPolicy", policy => {
                    policy.AllowAnyHeader()
                            .AllowAnyMethod()
                            .WithOrigins(apiSettingsData.ApiOriginUrl);
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            if (env.IsDevelopment())
            {
                app.UseSwaggerDoc();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();
            
            app.UseCors("CorsPolicy");
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
