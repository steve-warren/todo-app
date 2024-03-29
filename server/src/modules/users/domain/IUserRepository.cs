namespace WarrenSoftware.TodoApp.Modules.Users.Domain;

public interface IUserRepository
{
    Task<User> FindByIdAsync(int id);
    Task<User> FindByUserNameAsync(string userName);
}
