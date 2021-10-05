using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WarrenSoftware.TodoApp.Modules.Users
{
    public class GetCsrfToken : IRequest<string?>
    {
        public int UserId { get; init; }
    }

    public class SetCsrfTokenHandler : IRequestHandler<GetCsrfToken,string?>
    {
        private readonly IAntiforgery _antiforgery;
        private readonly ILogger<SetCsrfTokenHandler> _logger;
        private readonly HttpContext _httpContext;

        public SetCsrfTokenHandler(IAntiforgery antiforgery, IHttpContextAccessor httpContextAccessor, ILogger<SetCsrfTokenHandler> logger)
        {
            _antiforgery = antiforgery;
            _httpContext = httpContextAccessor.HttpContext;
            _logger = logger;
        }
        public Task<string?> Handle(GetCsrfToken request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CSRF token requested for user '{request.UserId}'.");
            
            var tokens = _antiforgery.GetAndStoreTokens(_httpContext);

            return Task.FromResult(tokens.RequestToken);
        }
    }
}