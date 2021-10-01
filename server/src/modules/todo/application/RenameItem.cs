using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo
{
    public class RenameItemCommand : IRequest
    {
        public int Id { get; init; }
        public string Name { get; init; } = "";
    }

    public class RenameItemHandler : AsyncRequestHandler<RenameItemCommand>
    {
        private readonly IIncompleteTodoItemRepository _items;
        private readonly IUnitOfWork _uow;

        public RenameItemHandler(IIncompleteTodoItemRepository items, IUnitOfWork uow)
        {
            _items = items; 
            _uow = uow;
        }

        protected override async Task Handle(RenameItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _items.FindByIdAsync(request.Id);

            if (item is null) return;

            item.Rename(request.Name);

            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}