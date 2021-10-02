using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo
{
    public class RenameItemCommand : IRequest<Unit>
    {
        public int ItemId { get; init; }
        public string Name { get; init; } = "";
    }

    public class RenameItemHandler : IRequestHandler<RenameItemCommand, Unit>
    {
        private readonly ITodoItemRepository _items;
        private readonly IUnitOfWork _uow;

        public RenameItemHandler(ITodoItemRepository items, IUnitOfWork uow)
        {
            _items = items; 
            _uow = uow;
        }

        public async Task<Unit> Handle(RenameItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _items.FindByIdAsync(request.ItemId);

            if (item is null) return Unit.Value;

            item.Rename(request.Name);

            await _uow.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}