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
            var list = new ActiveTodoList("a");

            list.Name.Should().Be("a", because: "the new list should use the name provided.");
        }

        [Fact]
        public void List_Should_Accept_New_Name()
        {
            var list = new ActiveTodoList("a");

            list.Rename("b");

            list.Name.Should().Be("b", because: "the list should use the new name provided.");
        }
    }
}
