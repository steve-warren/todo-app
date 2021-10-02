using Xunit;
using FluentAssertions;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;
using WarrenSoftware.TodoApp.Modules.Todo;
using WarrenSoftware.TodoApp.Tests.Core;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace todo_app_test
{
    public class TodoItemTests
    {
        [Fact]
        public void New_Item_Should_Accept_Valid_Name()
        {
            var list = new TodoItem("a", listId: 1, priority: TodoItemPriority.None, id: 2, notes: "", reminder: TimeConstants.DateAndTime);

            list.Name.Should().Be("a", because: "the new item should use the name provided.");
        }

        [Fact]
        public void Item_Should_Accept_New_Name_When_Renamed()
        {
            var list = new TodoItem("a", listId: 1, priority: TodoItemPriority.None, id: 2, notes: "", reminder: TimeConstants.DateAndTime);

            list.Rename("b");

            list.Name.Should().Be("b", because: "the item should use the new name provided.");
        }

        [Fact]
        public void Item_Should_Relocate_To_Another_List()
        {
            var item = new TodoItem("item", listId: 1, priority: TodoItemPriority.None, id: 2, notes: "", reminder: TimeConstants.DateAndTime);
            item.Relocate(listId: 2);

            item.ListId.Should().Be(2, because: "the item should have a different list id once relocated.");
        }

        [Fact]
        public async Task AddItem()
        {
            var command = new AddItemCommand
            {
                ListId = 1,
                Name = "foo",
                Notes = "notes",
                Priority = "Medium",
                Reminder = TimeConstants.DateAndTime
            };

            var lists = new MockTodoListRepository(new[] { new TodoList("list", 1 )});
            var items = new MockTodoItemRepository();
            var ids = new MockTodoItemIdentityService(id: 1);
            var uow = new UnitOfWorkSpy();

            var handler = new AddItemHandler(lists, items, ids, uow);
            var itemIdentity = await handler.Handle(command, CancellationToken.None);

            itemIdentity.Should().Be(expected: 1, because: "the id should be retrieved from the identity service.");
            uow.WasCalled.Should().BeTrue(because: "unit of work should be called.");

            var item = items.First();

            item.Id.Should().Be(ids.ReturnValue, because: "identity should be assigned from identity service.");
            item.ListId.Should().Be(command.ListId, because: "should value from command.");
            item.Name.Should().Be(command.Name, because: "should match value from command.");
            item.Notes.Should().Be(command.Notes, because: "should match value from command.");
            item.Priority.Should().Be(TodoItemPriority.Medium, because: "should match value from command.");
            item.Reminder.Should().Be(command.Reminder, because: "should match value from command.");
        }

        [Fact]
        public async Task EditItem()
        {
            var command = new EditItemCommand
            {
                ItemId = 1,
                Name = "foo",
                ListId = 1,
                Notes = "notes",
                Priority = "High",
                Reminder = TimeConstants.DateAndTime
            };

            var todoItem = new TodoItem(name: "", listId: 1, priority: TodoItemPriority.None, id: 1, notes: "", reminder: default);
            var todoList = new TodoList(name: "", id: 1 );


            var lists = new MockTodoListRepository(new[] { todoList });
            var items = new MockTodoItemRepository(new[] { todoItem });
            var uow = new UnitOfWorkSpy();

            var handler = new EditItemHandler(items, lists, uow);

            await handler.Handle(command, CancellationToken.None);

            uow.WasCalled.Should().BeTrue(because: "unit of work should be called.");

            todoItem.Id.Should().Be(command.ItemId, because: "it should match value from command.");
            todoItem.Name.Should().Be(command.Name, because: "it should match value from command.");
            todoItem.ListId.Should().Be(command.ListId, because: "it should match value from command.");
            todoItem.Notes.Should().Be(command.Notes, because: "it should match value from command.");
            todoItem.Priority.Should().Be(TodoItemPriority.High, because: "it should match value from command.");
            todoItem.Reminder.Should().Be(command.Reminder, because: "it should match value from command.");
        }
    }
}
