using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Core.Infrastructure;
using WarrenSoftware.TodoApp.Modules.Users.Domain;

namespace WarrenSoftware.TodoApp.Modules.Users.Infrastructure
{
    public class UserDbContext : DbContextBase
    {
        public UserDbContext(DbContextOptions options, IEventBus eventBus) : base(options, eventBus) { }

        public DbSet<User> Users { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var users = modelBuilder.Entity<User>();
            
            users.Property(e => e.Id)
                     .HasColumnName("Id")
                     .ValueGeneratedNever();


            users.ToTable("Users");
        }
    }
}