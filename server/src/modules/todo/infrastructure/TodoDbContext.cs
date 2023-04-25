using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Core.Infrastructure;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo.Infrastructure;

public class TodoDbContext : DbContextBase, ITodoUnitOfWork
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options, IEventBus eventBus) : base(options, eventBus) { }

    public DbSet<TodoList> TodoLists { get; private set; }
    public DbSet<TodoItem> TodoItems { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var todoLists = modelBuilder.Entity<TodoList>();
        var todoItems = modelBuilder.Entity<TodoItem>();

        todoLists.Property(e => e.Id)
                 .HasColumnName("Id")
                 .ValueGeneratedNever();

        var converter = new ValueConverter<List<int>, string>(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)default),
            v => JsonSerializer.Deserialize<List<int>>(v, (JsonSerializerOptions?)default)
        );

        var comparer = new ValueComparer<List<int>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => new List<int>(c));

        todoLists.Property("_items")
                 .HasColumnName("Items")
                 .HasConversion(converter, comparer);

        todoLists.Property<ArchiveState>("_archiveState")
                 .HasColumnName("ArchiveState")
                 .HasConversion(
                     v => v.Name,
                     v => ArchiveState.Parse(v)
                 );

        todoLists.ToTable("TodoLists");

        todoItems.Property<ArchiveState>("_archiveState")
                 .HasColumnName("ArchiveState")
                 .HasConversion(
                     v => v.Name,
                     v => ArchiveState.Parse(v)
                 );

        todoItems.Property(e => e.Priority)
                 .HasColumnName("Priority")
                 .HasConversion(
                     v => v.Name,
                     v => TodoItemPriority.Parse(v)
                 );

        todoItems.Property<TodoItemCompletedState>("_state")
                 .HasColumnName("State")
                 .HasConversion(
                     v => v.Name,
                     v => TodoItemCompletedState.Parse(v)
                 );

        todoItems.ToTable("TodoItems");
    }
}
