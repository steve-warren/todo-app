using System.Threading.Tasks;

namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public interface IActiveTodoListRepository
    {
        Task<ActiveTodoList> FindByIdAsync(int id);
        void Add(ActiveTodoList item);
        Task<bool> ExistsAsync(int id);
    }
}