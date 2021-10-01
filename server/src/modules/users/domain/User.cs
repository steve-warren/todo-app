
using WarrenSoftware.TodoApp.Core.Domain;

namespace WarrenSoftware.TodoApp.Modules.Users.Domain
{
    public abstract class User : AggregateRoot
    {
        protected User(string email)
        {
            Email = email;
        }
        
        protected User() { }
        public string Email { get; private set; } = "";
    }
}