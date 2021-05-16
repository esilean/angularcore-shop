using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AngularShop.API.Middleware
{
    public class LogHeaderMiddleware
    {
        private readonly RequestDelegate _next;
        public LogHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var headers = context.Request.Headers;

            var correlationId = headers["x-correlation-id"];
            if(string.IsNullOrWhiteSpace(correlationId))
                correlationId = Guid.NewGuid().ToString();

            var logger = context.RequestServices.GetRequiredService<ILogger<LogHeaderMiddleware>>();  
            using (logger.BeginScope("{@x-correlation-id}", correlationId))  
            {  
                await this._next(context);  
            }

            // using (LogContext.PushProperty("x-correlation-id", correlationId))
            // {
            //     await _next.Invoke(context);
            // }
        }
    }
}