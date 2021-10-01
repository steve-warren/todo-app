
using System;
using WarrenSoftware.TodoApp.Core.Domain;

namespace WarrenSoftware.TodoApp.Modules.Users.Domain
{
    public class UserLoggedIn : IDomainEvent
    {
        public int Id { get; init; }
        public DateTimeOffset When { get; init; }
    }
}