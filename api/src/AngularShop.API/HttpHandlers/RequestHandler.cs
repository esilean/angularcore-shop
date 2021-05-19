using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AngularShop.Application.Services.Accessors;

namespace AngularShop.API.HttpHandlers
{
    public class RequestHandler : DelegatingHandler
    {
        private readonly ICorrelationIdAccessor _correlationIdAccessor;

        public RequestHandler(ICorrelationIdAccessor correlationIdAccessor)
        {
            _correlationIdAccessor = correlationIdAccessor;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var correlationdId = _correlationIdAccessor.GetCorrelationId();

            request.Headers.Add("x-correlation-id", correlationdId);
            return base.SendAsync(request, cancellationToken);
        }
    }
}