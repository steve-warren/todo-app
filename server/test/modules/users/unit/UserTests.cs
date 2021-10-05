using System;
using Xunit;
using FluentAssertions;
using WarrenSoftware.TodoApp.Modules.Users.Domain;
using WarrenSoftware.TodoApp.Tests.Core;
using WarrenSoftware.TodoApp.Core.Domain;

namespace todo_app_test
{
    public class UserTests
    {
        private readonly MockSystemClock _clock = new() { Time = TimeConstants.DateAndTime };

        [Fact]
        public void Valid_Password_Should_Authenticate()
        {
            var user = new User(id: 1, email: "", userName: "", firstName: "", lastName: "");
            var authenticator = new MockAuthenticator { AuthenticationResultShouldBe = true };

            user.Login(_clock, authenticator, plaintextPassword: "foo")
                .Should()
                .Be(AuthenticationResult.Success, because: "the mock authenticator always returns true.");
        }

        [Fact]
        public void Invalid_Password_Should_Not_Authenticate()
        {
            var user = new User(id: 1, email: "", userName: "", firstName: "", lastName: "");
            var authentiator = new MockAuthenticator { AuthenticationResultShouldBe = false };

            user.Login(_clock, authentiator, plaintextPassword: "foo")
                .Should()
                .Be(AuthenticationResult.InvalidUserNameAndOrPassword, because: "the mock authenticator always returns false.");
        }

        [Fact]
        public void Successful_Authentication_Should_Update_Last_Login_Date()
        {
            var user = new User(id: 1, email: "", userName: "", firstName: "", lastName: "");
            var authenticator = new MockAuthenticator { AuthenticationResultShouldBe = true };

            user.Login(_clock, authenticator, plaintextPassword: "foo");

            user.LastLoginDate
                .Should()
                .Be(_clock.Time, because: "the user successfully logged in and the timestamp should be updated.");
        }

        [Fact]
        public void Successful_Authentication_Should_Raise_Event()
        {
            var user = new User(id: 1, email: "", userName: "", firstName: "", lastName: "");
            var authenticator = new MockAuthenticator { AuthenticationResultShouldBe = true };

            user.Login(_clock, authenticator, plaintextPassword: "foo");

            user.DomainEvents
                .Should()
                .ContainItemsAssignableTo<UserLoggedIn>(because: "the user successfully logged in.");
        }
    }
}
