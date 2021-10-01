using System.Threading.Tasks;

namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public interface ITodoItemRepository
    {
        Task<TodoItem> FindByIdAsync(int id);
        void Add(TodoItem item);
    }
}