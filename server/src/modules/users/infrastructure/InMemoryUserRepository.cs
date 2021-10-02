using System.Threading.Tasks;
using WarrenSoftware.TodoApp.Modules.Users.Domain;

namespace WarrenSoftware.TodoApp.Modules.Users.Infrastructure
{
    public class InMemoryUserRepository : IUserRepository
    {
        public Task<UnauthenticatedUser> FindUnauthenticatedUserByEmailAsync(string email)
        {
            throw new System.NotImplementedException();
        }
    }
}