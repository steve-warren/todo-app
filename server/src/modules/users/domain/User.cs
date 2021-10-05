
using System;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Core.Infrastructure;

namespace WarrenSoftware.TodoApp.Modules.Users.Domain
{
    public class User : AggregateRoot
    {
        private string _hashedPassword = "";

        public User(int id, string email, string userName, string firstName, string lastName)
        {
            Id = id;
            Email = email;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
        }
        
        private User() { }
        public string Email { get; private set; } = "";
        public string UserName { get; private set; } = "";
        public string FirstName { get; private set; } = "";
        public string LastName { get; private set; } = "";
        public DateTimeOffset? LastLoginDate { get; private set; }

        public void ChangePassword(IAuthenticator authenticator, string plaintextPassword)
        {
            _hashedPassword = authenticator.HashPassword(plaintextPassword);
        }

        public AuthenticationResult Login(ISystemClock clock, IAuthenticator authenticator, string plaintextPassword)
        {
            if (authenticator.Authenticate(plaintextPassword, _hashedPassword) is false)
                return AuthenticationResult.InvalidUserNameAndOrPassword;

            var now = clock.Now();
            LastLoginDate = now;
            Apply( new UserLoggedIn { UserId = Id, When = now });

            return AuthenticationResult.Success;
        }

        public void LogOut()
        {
            Apply(new UserLoggedOut { Id = Id });
        }
    }
}