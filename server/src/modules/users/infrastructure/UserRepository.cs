using Microsoft.EntityFrameworkCore;
using WarrenSoftware.TodoApp.Modules.Users.Domain;

namespace WarrenSoftware.TodoApp.Modules.Users.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly DbSet<User> _users;

    public UserRepository(UserDbContext context)
    {
        _users = context.Users;
    }

    public Task<User> FindByUserNameAsync(string userName)
    {
        return _users.FirstOrDefaultAsync(user => user.UserName == userName);
    }

    public Task<User> FindByIdAsync(int id)
    {
        return _users.FirstOrDefaultAsync(user => user.Id == id);
    }
}
