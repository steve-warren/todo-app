using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Core.Infrastructure;
using WarrenSoftware.TodoApp.Modules.Users.Domain;

namespace WarrenSoftware.TodoApp.Modules.Users
{
    public class AuthenticateCommand : IRequest<bool>
    {
        public string Email { get; set; } = "";
        public string PlaintextPassword { get; set; } = "";
    }

    public class AuthenticateHandler : IRequestHandler<AuthenticateCommand, bool>
    {
        private readonly IUnauthenticatedUserRepository _users;
        private readonly IUnitOfWork _uow;
        private readonly IAuthenticator _authenticator;
        private readonly ISystemClock _clock;

        public AuthenticateHandler(IUnauthenticatedUserRepository users, IUnitOfWork uow, IAuthenticator authenticator, ISystemClock clock)
        {
            _users = users;
            _uow = uow;
            _authenticator = authenticator;
            _clock = clock;
        }

        public async Task<bool> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var user = await _users.FindByEmailAsync(request.Email);

            if (user.Authenticate(_clock, _authenticator, request.PlaintextPassword))
            {
                await _uow.SaveChangesAsync(cancellationToken);
                return true;
            }

            return false;
        }
    }
}