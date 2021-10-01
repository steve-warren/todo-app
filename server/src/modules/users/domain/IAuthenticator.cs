namespace WarrenSoftware.TodoApp.Modules.Users.Domain
{
    public interface IAuthenticator
    {
        bool Authenticate(string plaintextPassword, string hash);
    }
}