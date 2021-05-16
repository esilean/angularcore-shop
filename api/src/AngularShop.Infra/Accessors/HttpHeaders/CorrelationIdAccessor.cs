using AngularShop.Application.Accessors.HttpHeaders;
using Microsoft.AspNetCore.Http;

namespace AngularShop.Infra.Accessors.HttpHeaders
{
    public class CorrelationIdAccessor : ICorrelationIdAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CorrelationIdAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

        }
        public string GetCorrelationId()
        {
            _httpContextAccessor.HttpContext.Items.TryGetValue("x-correlation-id", out var correlationId);
            return correlationId.ToString();
        }
    }
}