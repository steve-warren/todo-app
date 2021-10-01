using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo
{
    public class RelocateItemCommand : IRequest
    {
        public int ListId { get; init; }
        public int ItemId { get; init; }
        public string Name { get; init; } = "";
    }

    public class RelocateItemHandler : AsyncRequestHandler<RelocateItemCommand>
    {
        private readonly IIncompleteTodoItemRepository _items;
        private readonly IActiveTodoListRepository _lists;
        private readonly IUnitOfWork _uow;

        public RelocateItemHandler(IIncompleteTodoItemRepository items, IActiveTodoListRepository lists, IUnitOfWork uow)
        {
            _items = items;
            _lists = lists;
            _uow = uow;
        }

        protected override async Task Handle(RelocateItemCommand request, CancellationToken cancellationToken)
        {
            var listExists = await _lists.ExistsAsync(request.ListId);

            if (listExists is false) return;

            var item = await _items.FindByIdAsync(request.ItemId);

            if (item is null) return;

            item.Relocate(request.ListId);

            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}