using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using AngularShop.Application.Errors;
using AngularShop.Core.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AngularShop.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next,
                                  ILogger<ExceptionMiddleware> logger,
                                  IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                int statusCode = (int)HttpStatusCode.InternalServerError;
                switch (ex)
                {
                    case SomeException se:
                        statusCode = se.StatusCode;
                        break;
                    default:
                        break;
                };

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;
                var response = _env.IsDevelopment()
                     ? new ApiException(statusCode, ex.Message, ex.StackTrace.ToString())
                     : new ApiException(statusCode);

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}