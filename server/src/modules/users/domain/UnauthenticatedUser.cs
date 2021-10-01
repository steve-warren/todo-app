using System;
using WarrenSoftware.TodoApp.Core.Infrastructure;

namespace WarrenSoftware.TodoApp.Modules.Users.Domain
{
    public class UnauthenticatedUser : User
    {
        public UnauthenticatedUser(string email, string hashedPassword)
            :base(email)
        {
            HashedPassword = hashedPassword;
        }

        private string HashedPassword { get; set; } = "";
        public DateTimeOffset? LastLoginDate { get; private set; }

        public bool Authenticate(ISystemClock clock, IAuthenticator authenticator, string plaintextPassword)
        {
            if (authenticator.Authenticate(plaintextPassword, this.HashedPassword) is false) return false;

            var now = clock.Now();
            LastLoginDate = now;
            Apply( new UserLoggedIn { Id = Id, When = now });

            return true;
        }

        public void Login(DateTimeOffset now)
        {
            LastLoginDate = now;
            Apply(new UserLoggedIn { Id = Id, When = now });
        }
    }
}