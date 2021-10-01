using System.Threading.Tasks;

namespace WarrenSoftware.TodoApp.Modules.Users.Domain
{
    public interface IUnauthenticatedUserRepository
    {
        Task<UnauthenticatedUser> FindByEmailAsync(string email);
    }
}