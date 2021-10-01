using System;
using Xunit;
using FluentAssertions;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;

namespace todo_app_test
{
    public class ActiveTodoListTests
    {
        [Fact]
        public void New_List_Should_Accept_Valid_Name()
        {
            var list = new ActiveTodoList(name: "a", id: 1);

            list.Name.Should().Be("a", because: "the new list should use the name provided.");
        }

        [Fact]
        public void List_Should_Accept_New_Name_When_Renamed()
        {
            var list = new ActiveTodoList(name: "a", id: 1);

            list.Rename("b");

            list.Name.Should().Be("b", because: "the list should use the new name provided.");
        }

        [Fact]
        public void Item_Should_Be_Added_To_Top_Of_List()
        {
            var list = new ActiveTodoList(name: "a", id: 1);

            list.AddItem(itemId: 1111);
            list.AddItem(itemId: 2222);
            list.AddItem(itemId: 3333);

            list.Items
                .Should()
                .ContainInOrder(new[] { 3333, 2222, 1111 }, because: "items, by default, are added to the top of the list.");
        }

        [Fact]
        public void Item_Should_Be_Moved_To_Specified_Position()
        {
            var list = new ActiveTodoList(name: "a", id: 1);

            list.AddItem(itemId: 1111);
            list.AddItem(itemId: 2222);
            list.AddItem(itemId: 3333);

            list.MoveItem(itemId: 3333, 1);

            list.Items
                .Should()
                .ContainInOrder(new[] { 2222, 3333, 1111 }, because: "item was inserted into position 1.");
        }
    }
}
