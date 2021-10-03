using Microsoft.EntityFrameworkCore;
using WarrenSoftware.TodoApp.Core.Infrastructure;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo.Infrastructure
{
    public class TodoDbContext : DbContextBase
    {
        public TodoDbContext(DbContextOptions options, IEventBus eventBus) : base(options, eventBus) { }

        public DbSet<TodoList> TodoLists { get; private set; }
        public DbSet<TodoItem> TodoItems { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoList>().ToTable("TodoLists");
            modelBuilder.Entity<TodoItem>().ToTable("TodoItems");
        }
    }
}