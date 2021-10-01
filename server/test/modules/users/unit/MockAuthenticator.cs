using WarrenSoftware.TodoApp.Modules.Users.Domain;

namespace todo_app_test
{
    public class MockAuthenticator : IAuthenticator
    {
        public bool AuthenticationResultShouldBe { get; init; }
        public bool Authenticate(string plaintextPassword, string hash) => AuthenticationResultShouldBe;
    }
}
