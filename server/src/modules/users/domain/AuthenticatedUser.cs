namespace WarrenSoftware.TodoApp.Modules.Users.Domain
{
    public class AuthenticatedUser : User
    {
        public string FirstName { get; private set; } = "";
        public string LastName { get; private set; } = "";

        public void LogOut()
        {
            Apply(new UserLoggedOut { Id = Id });
        }
    }
}