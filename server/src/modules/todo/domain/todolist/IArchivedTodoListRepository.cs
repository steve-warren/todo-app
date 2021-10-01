using System.Threading.Tasks;

namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public interface IArchivedTodoListRepository
    {
        Task<ArchivedTodoList> FindByIdAsync(int id);
    }
}