using System.Threading.Tasks;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo.Infrastructure
{
    public class InMemoryTodoItemRepository : ITodoItemRepository
    {
        public void Add(TodoItem item)
        {
            
        }

        public void Delete(TodoItem item)
        {
            
        }

        public Task<TodoItem> FindByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<CompletedTodoItem> FindCompletedByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IncompleteTodoItem> FindIncompleteByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}