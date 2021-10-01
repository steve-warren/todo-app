using System.Threading.Tasks;

namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public interface INotArchivedTodoItemRepository
    {
        Task<NotArchivedTodoItem> FindByIdAsync(int id);
    }
}