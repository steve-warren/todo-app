using System.Threading.Tasks;

namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public interface IArchivedTodoItemRepository
    {
        Task<ArchivedTodoItem> FindByIdAsync(int id);
    }
}