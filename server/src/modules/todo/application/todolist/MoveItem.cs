using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;
using WarrenSoftware.TodoApp.Modules.Todo.Infrastructure;

namespace WarrenSoftware.TodoApp.Modules.Todo
{
    public class MoveItemCommand : IRequest
    {
        public int ListId { get; init; }
        public int ItemId { get; init; }
        public int Position { get; init; }
    }

    public class MoveItemHandler : AsyncRequestHandler<MoveItemCommand>
    {
        private readonly ITodoListRepository _lists;
        private readonly ITodoUnitOfWork _uow;

        public MoveItemHandler(ITodoListRepository lists, ITodoUnitOfWork uow)
        {
            _lists = lists;
            _uow = uow;
        }

        protected override async Task Handle(MoveItemCommand request, CancellationToken cancellationToken)
        {
            var list = await _lists.FindByIdAsync(request.ListId);

            if (list is null) return;

            list.MoveItem(request.ItemId, request.Position);

            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}