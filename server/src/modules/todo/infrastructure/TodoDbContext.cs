using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
            var todoLists = modelBuilder.Entity<TodoList>();
            var todoItems = modelBuilder.Entity<TodoItem>();
            
            todoLists.Property(e => e.Id)
                     .HasColumnName("Id")
                     .ValueGeneratedNever();

            var comparer = new ValueComparer<List<int>>(
                            (c1, c2) => c1.SequenceEqual(c2),
                            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                            c => new List<int>(c));

            todoLists.Property(e => e.Items)
                     .HasColumnName("Items")
                     .HasConversion(
                         v => JsonSerializer.Serialize(v, null),
                         v => JsonSerializer.Deserialize<List<int>>(v, null),
                         comparer);
            
            todoLists.Property<string>("_archiveState")
                     .HasColumnName("ArchiveState");

            todoLists.ToTable("TodoLists");
            
            todoItems.Property(e => e.Priority)
                     .HasColumnName("Priority")
                     .HasConversion(
                         v => v.Name,
                         v => TodoItemPriority.Parse(v)
                     );
            
            todoItems.Property<string>("_state")
                     .HasColumnName("State");

            todoItems.ToTable("TodoItems");
        }
    }
}