using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Core.Infrastructure;
using WarrenSoftware.TodoApp.Modules.Users.Domain;
using WarrenSoftware.TodoApp.Modules.Users.Infrastructure;

namespace WarrenSoftware.TodoApp.Modules.Users
{
    public class LogOutUserCommand : IRequest<Unit>
    {
        public int UserId { get; init; }
    }

    public class LogOutUserHandler : IRequestHandler<LogOutUserCommand>
    {
        private readonly IUserRepository _users;

        public LogOutUserHandler(IUserRepository users)
        {
            _users = users;
        }

        public async Task<Unit> Handle(LogOutUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _users.FindByIdAsync(request.UserId);

            user.LogOut();

            return Unit.Value;
        }
    }
}