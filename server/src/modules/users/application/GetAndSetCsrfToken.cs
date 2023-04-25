using MediatR;
using Microsoft.AspNetCore.Antiforgery;

namespace WarrenSoftware.TodoApp.Modules.Users;

public class GetAndSetCsrfToken : IRequest<string?>
{
    public int UserId { get; init; }
}

public class GetAndSetCsrfTokenHandler : IRequestHandler<GetAndSetCsrfToken, string?>
{
    private readonly IAntiforgery _antiforgery;
    private readonly ILogger<GetAndSetCsrfTokenHandler> _logger;
    private readonly HttpContext _httpContext;

    public GetAndSetCsrfTokenHandler(IAntiforgery antiforgery, IHttpContextAccessor httpContextAccessor, ILogger<GetAndSetCsrfTokenHandler> logger)
    {
        _antiforgery = antiforgery;
        _httpContext = httpContextAccessor.HttpContext;
        _logger = logger;
    }
    public Task<string?> Handle(GetAndSetCsrfToken request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"CSRF token requested for user '{request.UserId}'.");

        var tokens = _antiforgery.GetAndStoreTokens(_httpContext);

        return Task.FromResult(tokens.RequestToken);
    }
}
