namespace WarrenSoftware.TodoApp.Modules.Todo.Domain;

public interface ITodoListRepository
{
    Task<TodoList> FindByIdAsync(int id);
    void Add(TodoList item);
    Task<bool> ExistsAsync(int id);
}
