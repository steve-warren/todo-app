using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo.Infrastructure
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly DbSet<TodoItem> items;

        public TodoItemRepository(TodoDbContext context)
        {
            items = context.TodoItems;
        }
        public void Add(TodoItem item)
        {
            items.Add(item);
        }

        public void Remove(TodoItem item)
        {
            items.Remove(item);
        }

        public Task<TodoItem> FindByIdAsync(int id)
        {
            return items.FirstOrDefaultAsync(item => item.Id == id);
        }
    }
}