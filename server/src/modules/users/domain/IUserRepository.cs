using System.Threading.Tasks;

namespace WarrenSoftware.TodoApp.Modules.Users.Domain
{
    public interface IUserRepository
    {
        Task<UnauthenticatedUser> FindUnauthenticatedUserByEmailAsync(string email);
    }
}