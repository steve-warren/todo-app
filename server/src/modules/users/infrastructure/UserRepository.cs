using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WarrenSoftware.TodoApp.Modules.Users.Domain;

namespace WarrenSoftware.TodoApp.Modules.Users.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly DbSet<User> _users;

        public UserRepository(UserDbContext context)
        {
            _users = context.Users;
        }

        public Task<User> FindByEmailAsync(string email)
        {
            return _users.FirstOrDefaultAsync(user => user.Email == email);
        }
    }
}