using WarrenSoftware.TodoApp.Modules.Users.Domain;

namespace WarrenSoftware.TodoApp.Modules.Users.Infrastructure
{
    public sealed class BCryptAuthenticator : IAuthenticator
    {
        public bool Authenticate(string plaintextPassword, string hash) => BCrypt.Net.BCrypt.EnhancedVerify(text: plaintextPassword, hash: hash);
    }
}