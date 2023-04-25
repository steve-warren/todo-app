
using MediatR;
using Microsoft.AspNetCore.Authentication;
using WarrenSoftware.TodoApp.Core.Domain;

namespace WarrenSoftware.TodoApp.Modules.Users.Domain;

public class UserLoggedOut : IDomainEvent
{
    public int Id { get; init; }
}

public class UserLoggedOutHandler : INotificationHandler<UserLoggedOut>
{
    private readonly HttpContext _httpContext;

    public UserLoggedOutHandler(IHttpContextAccessor httpContextAccessor)
    {
        if (httpContextAccessor.HttpContext is null) throw new ArgumentException("HttpContext is null.");

        _httpContext = httpContextAccessor.HttpContext;
    }

    public Task Handle(UserLoggedOut notification, CancellationToken cancellationToken)
    {
        return _httpContext.SignOutAsync();
    }
}
