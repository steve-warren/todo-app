using MediatR;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo;

public class ArrangeItemWhenItemCreatedCommand : INotificationHandler<TodoItemCreated>
{
    private readonly ITodoListRepository _lists;

    public ArrangeItemWhenItemCreatedCommand(ITodoListRepository lists)
    {
        _lists = lists;
    }

    public async Task Handle(TodoItemCreated notification, CancellationToken cancellationToken)
    {
        var list = await _lists.FindByIdAsync(notification.ListId);

        if (list is null) return;

        list.AddItem(notification.ItemId);
    }
}
