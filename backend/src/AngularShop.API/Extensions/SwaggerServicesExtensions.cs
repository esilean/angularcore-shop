using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace AngularShop.API.Extensions
{
    public static class SwaggerServicesExtensions
    {
        public static IServiceCollection AddSwaggerDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AngularShop", Version = "v1" });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDoc(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AngularShop v1"));
            
            return app;
        }
    }
}