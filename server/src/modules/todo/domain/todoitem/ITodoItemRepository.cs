using System.Threading.Tasks;

namespace WarrenSoftware.TodoApp.Modules.Todo.Domain
{
    public interface IIncompleteTodoItemRepository
    {
        Task<IncompleteTodoItem> FindByIdAsync(int id);
        void Add(IncompleteTodoItem item);
    }
}