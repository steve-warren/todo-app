using System;
using Xunit;
using FluentAssertions;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;

namespace todo_app_test
{
    public class TodoItemTests
    {
        [Fact]
        public void New_Item_Should_Accept_Valid_Name()
        {
            var list = new TodoItem("a", listId: 1, priority: TodoItemPriority.None, id: 2);

            list.Name.Should().Be("a", because: "the new item should use the name provided.");
        }

        [Fact]
        public void Item_Should_Accept_New_Name_When_Renamed()
        {
            var list = new TodoItem("a", listId: 1, priority: TodoItemPriority.None, id: 2);

            list.Rename("b");

            list.Name.Should().Be("b", because: "the item should use the new name provided.");
        }

        [Fact]
        public void Item_Should_Relocate_To_Another_List()
        {
            var item = new TodoItem("item", listId: 1, priority: TodoItemPriority.None, id: 2);
            item.Relocate(listId: 2);

            item.ListId.Should().Be(2, because: "the item should have a different list id once relocated.");
        }
    }
}
