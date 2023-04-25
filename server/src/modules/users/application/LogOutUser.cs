using MediatR;
using WarrenSoftware.TodoApp.Modules.Users.Domain;
using WarrenSoftware.TodoApp.Modules.Users.Infrastructure;

namespace WarrenSoftware.TodoApp.Modules.Users
{
    public class LogOutUserCommand : IRequest
    {
        public int UserId { get; init; }
    }

    public class LogOutUserHandler : IRequestHandler<LogOutUserCommand>
    {
        private readonly IUserRepository _users;
        private readonly IUserUnitOfWork _uow;

        public LogOutUserHandler(IUserRepository users, IUserUnitOfWork uow)
        {
            _users = users;
            _uow = uow;
        }

        public async Task Handle(LogOutUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _users.FindByIdAsync(request.UserId);

            if (user is null) return;

            user.LogOut();

            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}