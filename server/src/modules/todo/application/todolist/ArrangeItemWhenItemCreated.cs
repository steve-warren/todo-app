using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;
using WarrenSoftware.TodoApp.Modules.Todo.Infrastructure;

namespace WarrenSoftware.TodoApp.Modules.Todo
{
    public class ArrangeItemWhenItemCreated : INotificationHandler<TodoItemCreated>
    {
        private readonly ITodoListRepository _lists;
        private readonly ITodoUnitOfWork _uow;

        public ArrangeItemWhenItemCreated(ITodoListRepository lists, ITodoUnitOfWork uow)
        {
            _lists = lists;
            _uow = uow;
        }

        public async Task Handle(TodoItemCreated notification, CancellationToken cancellationToken)
        {
            var list = await _lists.FindByIdAsync(notification.ListId);

            if (list is null) return;

            list.AddItem(notification.ItemId);

            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}