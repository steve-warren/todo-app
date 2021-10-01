using System;
using Xunit;
using FluentAssertions;
using WarrenSoftware.TodoApp.Modules.Users.Domain;
using WarrenSoftware.TodoApp.Tests.Core;

namespace todo_app_test
{
    public class UserTests
    {
        private readonly MockSystemClock _clock = new() { Time = new DateTimeOffset(2021, 09, 30, 23, 06, 0, TimeSpan.FromHours(-5)) };

        [Fact]
        public void Valid_Password_Should_Authenticate()
        {
            var user = new UnauthenticatedUser(email: "email", hashedPassword: "hash");
            var authenticator = new MockAuthenticator { AuthenticationResultShouldBe = true };

            user.Authenticate(_clock, authenticator, plaintextPassword: "foo")
                .Should()
                .BeTrue(because: "the mock authenticator always returns true.");
        }

        [Fact]
        public void Invalid_Password_Should_Not_Authenticate()
        {
            var user = new UnauthenticatedUser(email: "email", hashedPassword: "hash");
            var authentiator = new MockAuthenticator { AuthenticationResultShouldBe = false };

            user.Authenticate(_clock, authentiator, plaintextPassword: "foo")
                .Should()
                .BeFalse(because: "the mock authenticator always returns false.");
        }

        [Fact]
        public void Successful_Authentication_Should_Update_Last_Login_Date()
        {
            var user = new UnauthenticatedUser(email: "email", hashedPassword:"hash");
            var authenticator = new MockAuthenticator { AuthenticationResultShouldBe = true };

            user.Authenticate(_clock, authenticator, plaintextPassword: "foo");

            user.LastLoginDate
                .Should()
                .Be(_clock.Time, because: "the user successfully logged in and the timestamp should be updated.");
        }

        [Fact]
        public void Successful_Authentication_Should_Raise_Event()
        {
            var user = new UnauthenticatedUser(email: "email", hashedPassword: "hash");
            var authenticator = new MockAuthenticator { AuthenticationResultShouldBe = true };

            user.Authenticate(_clock, authenticator, plaintextPassword: "foo");

            user.DomainEvents
                .Should()
                .ContainItemsAssignableTo<UserLoggedIn>(because: "the user successfully logged in.");
        }
    }
}
