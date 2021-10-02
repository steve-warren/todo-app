using System.Threading.Tasks;

namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public interface ITodoItemRepository
    {
        Task<TodoItem> FindByIdAsync(int id);
        Task<IncompleteTodoItem> FindIncompleteByIdAsync(int id);
        Task<CompletedTodoItem> FindCompletedByIdAsync(int id);
        void Add(TodoItem item);
        void Delete(TodoItem item);
    }
}