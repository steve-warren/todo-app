
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using WarrenSoftware.TodoApp.Core.Domain;

namespace WarrenSoftware.TodoApp.Modules.Users.Domain;

public class UserLoggedIn : IDomainEvent
{
    public int UserId { get; init; }
    public DateTimeOffset When { get; init; }
}

public class UserLoggedInHandler : INotificationHandler<UserLoggedIn>
{
    private readonly HttpContext _httpContext;

    public UserLoggedInHandler(IHttpContextAccessor httpContextAccessor)
    {
        if (httpContextAccessor.HttpContext is null) throw new ArgumentException("HttpContext is null.");

        _httpContext = httpContextAccessor.HttpContext;
    }

    public Task Handle(UserLoggedIn notification, CancellationToken cancellationToken)
    {
        var claims = new List<Claim>
            {
                new Claim("id", notification.UserId.ToString())
            };

        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

        return _httpContext.SignInAsync(claimsPrincipal, new AuthenticationProperties
        {
            IsPersistent = true,
            AllowRefresh = true
        });
    }
}
