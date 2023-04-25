using WarrenSoftware.TodoApp.Modules.Todo.Domain;

namespace todo_app_test;

public class MockTodoItemRepository : MockRepository<TodoItem>, ITodoItemRepository
{
    public MockTodoItemRepository()
        : base() { }
    public MockTodoItemRepository(IEnumerable<TodoItem> items)
        : base(items) { }

    public void Add(TodoItem item) => Items.Add(item);

    public void Remove(TodoItem item) => Items.Remove(item);

    public Task<TodoItem> FindByIdAsync(int id) => Task.FromResult(Items.FirstOrDefault(l => l.Id == id));
}
