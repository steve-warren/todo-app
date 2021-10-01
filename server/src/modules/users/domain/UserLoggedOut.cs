
using WarrenSoftware.TodoApp.Core.Domain;

namespace WarrenSoftware.TodoApp.Modules.Users.Domain
{
    public class UserLoggedOut : IDomainEvent
    {
        public int Id { get; init; }
    }
}