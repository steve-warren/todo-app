using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo.Infrastructure
{
    public class TodoListRepository : ITodoListRepository
    {
        private readonly DbSet<TodoList> _lists;

        public TodoListRepository(TodoDbContext context)
        {
            _lists = context.TodoLists;
        }
        public void Add(TodoList item)
        {
            _lists.Add(item);
        }

        public Task<bool> ExistsAsync(int id)
        {
            return _lists.AnyAsync(item => item.Id == id);
        }

        public Task<TodoList> FindByIdAsync(int id)
        {
            return _lists.FirstOrDefaultAsync(item => item.Id == id);
        }
    }
}