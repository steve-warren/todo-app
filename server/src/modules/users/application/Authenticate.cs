using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Core.Infrastructure;
using WarrenSoftware.TodoApp.Modules.Users.Domain;
using WarrenSoftware.TodoApp.Modules.Users.Infrastructure;

namespace WarrenSoftware.TodoApp.Modules.Users
{
    public class AuthenticateCommand : IRequest<int>
    {
        public string UserName { get; set; } = "";
        public string PlaintextPassword { get; set; } = "";
    }

    public class AuthenticateHandler : IRequestHandler<AuthenticateCommand, int>
    {
        private readonly IUserRepository _users;
        private readonly IUserUnitOfWork _uow;
        private readonly IAuthenticator _authenticator;
        private readonly ISystemClock _clock;
        private readonly ILogger<AuthenticateHandler> _logger;
        private const int INVALID_USER_ID = -1;

        public AuthenticateHandler(IUserRepository users, IUserUnitOfWork uow, IAuthenticator authenticator, ISystemClock clock, ILogger<AuthenticateHandler> logger)
        {
            _users = users;
            _uow = uow;
            _authenticator = authenticator;
            _clock = clock;
            _logger = logger;
        }

        public async Task<int> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Authenticating user '{request.UserName}'.");

            var user = await _users.FindByUserNameAsync(request.UserName);

            if (user is not null)
            {
                var authenticationResult = user.Login(_clock, _authenticator, request.PlaintextPassword);

                await _uow.SaveChangesAsync(cancellationToken);

                if (authenticationResult == AuthenticationResult.Success)
                {
                    _logger.LogInformation($"Authentication successful for user '{request.UserName}'.");
                    return user.Id;
                }
            }

            _logger.LogInformation($"Authentication failed for user '{request.UserName}'.");

            return INVALID_USER_ID;
        }
    }
}