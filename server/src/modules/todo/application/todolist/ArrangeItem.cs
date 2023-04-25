using MediatR;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;
using WarrenSoftware.TodoApp.Modules.Todo.Infrastructure;

namespace WarrenSoftware.TodoApp.Modules.Todo
{
    public class ArrangeItemCommand : IRequest
    {
        public int ListId { get; init; }
        public int ItemId { get; init; }
        public int Position { get; init; }
    }

    public class ArrangeItemHandler : IRequestHandler<ArrangeItemCommand>
    {
        private readonly ITodoListRepository _lists;
        private readonly ITodoUnitOfWork _uow;

        public ArrangeItemHandler(ITodoListRepository lists, ITodoUnitOfWork uow)
        {
            _lists = lists;
            _uow = uow;
        }

        public async Task Handle(ArrangeItemCommand request, CancellationToken cancellationToken)
        {
            var list = await _lists.FindByIdAsync(request.ListId);

            if (list is null) return;

            list.ArrangeItem(request.ItemId, request.Position);

            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}