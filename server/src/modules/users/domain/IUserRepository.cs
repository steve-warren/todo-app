using System.Threading.Tasks;

namespace WarrenSoftware.TodoApp.Modules.Users.Domain
{
    public interface IUserRepository
    {
        Task<User> FindByEmailAsync(string email);
    }
}