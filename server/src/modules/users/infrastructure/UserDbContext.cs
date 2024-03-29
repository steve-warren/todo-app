using Microsoft.EntityFrameworkCore;
using WarrenSoftware.TodoApp.Core.Infrastructure;
using WarrenSoftware.TodoApp.Modules.Users.Domain;

namespace WarrenSoftware.TodoApp.Modules.Users.Infrastructure;

public class UserDbContext : DbContextBase, IUserUnitOfWork
{
    public UserDbContext(DbContextOptions<UserDbContext> options, IEventBus eventBus) : base(options, eventBus) { }

    public DbSet<User> Users { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var users = modelBuilder.Entity<User>();

        users.Property(e => e.Id)
                 .HasColumnName("Id")
                 .ValueGeneratedNever();

        users.Property<string>("_hashedPassword")
             .HasColumnName("HashedPassword");

        users.ToTable("Users");
    }
}
