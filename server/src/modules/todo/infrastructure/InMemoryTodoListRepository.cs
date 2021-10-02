using System.Threading.Tasks;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo.Infrastructure
{
    public class InMemoryTodoListRepository : ITodoListRepository
    {
        public void Add(TodoList item)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<TodoList> FindByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}