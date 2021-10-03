using System;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace todo_app_test
{
    public class MockTodoItemRepository : MockRepository<TodoItem>, ITodoItemRepository
    {
        public MockTodoItemRepository()
            :base() { }
        public MockTodoItemRepository(IEnumerable<TodoItem> items)
            :base(items) { }
            
        public void Add(TodoItem item) => Items.Add(item);

        public void Remove(TodoItem item) => Items.Remove(item);

        public Task<TodoItem> FindByIdAsync(int id) => Task.FromResult(Items.FirstOrDefault(l => l.Id == id));

        public Task<CompletedTodoItem> FindCompletedByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IncompleteTodoItem> FindIncompleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
