using WarrenSoftware.TodoApp.Modules.Todo.Domain;

namespace todo_app_test;

public class MockTodoListRepository : MockRepository<TodoList>, ITodoListRepository
{
    public MockTodoListRepository(IEnumerable<TodoList> items)
        : base(items) { }

    public void Add(TodoList item) => Items.Add(item);

    public Task<bool> ExistsAsync(int id) => Task.FromResult(Items.Exists(l => l.Id == id));

    public Task<TodoList> FindByIdAsync(int id) => Task.FromResult(Items.FirstOrDefault(l => l.Id == id));
}
