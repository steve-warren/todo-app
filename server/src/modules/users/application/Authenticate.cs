using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Core.Infrastructure;
using WarrenSoftware.TodoApp.Modules.Users.Domain;
using WarrenSoftware.TodoApp.Modules.Users.Infrastructure;

namespace WarrenSoftware.TodoApp.Modules.Users
{
    public class AuthenticateCommand : IRequest<int>
    {
        public string Email { get; set; } = "";
        public string PlaintextPassword { get; set; } = "";
    }

    public class AuthenticateHandler : IRequestHandler<AuthenticateCommand, int>
    {
        private readonly IUserRepository _users;
        private readonly IUserUnitOfWork _uow;
        private readonly IAuthenticator _authenticator;
        private readonly ISystemClock _clock;

        public AuthenticateHandler(IUserRepository users, IUserUnitOfWork uow, IAuthenticator authenticator, ISystemClock clock)
        {
            _users = users;
            _uow = uow;
            _authenticator = authenticator;
            _clock = clock;
        }

        public async Task<int> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var user = await _users.FindByEmailAsync(request.Email);

            var authenticationResult = user.Login(_clock, _authenticator, request.PlaintextPassword);

            await _uow.SaveChangesAsync(cancellationToken);

            if (authenticationResult == AuthenticationResult.Success)
                return user.Id;

            return -1;
        }
    }
}