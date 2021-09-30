using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo
{
    public class AddItemCommand : IRequest<int>
    {
        public int ListId { get; init; }
        public string ItemName { get; init; } = "";
    }

    public class AddItemHandler : IRequestHandler<AddItemCommand, int>
    {
        private readonly IActiveTodoListRepository _lists;
        private readonly ITodoItemRepository _items;
        private readonly IUnitOfWork _uow;

        public AddItemHandler(IActiveTodoListRepository lists, ITodoItemRepository items, IUnitOfWork uow)
        {
            _lists = lists;
            _items = items; 
            _uow = uow;
        }

        public async Task<int> Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            var list = await _lists.FindByIdAsync(request.ListId);

            if (list is null) return -1;

            var item = list.NewItem(request.ItemName);

            await _items.AddAsync(item);

            await _uow.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}