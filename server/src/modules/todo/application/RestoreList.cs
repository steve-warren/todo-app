using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Modules.Todo.Domain;

namespace WarrenSoftware.TodoApp.Modules.Todo
{
    public class RestoreListCommand : IRequest
    {
        public int ListId { get; init; }
    }

    public class RestoreListHandler : AsyncRequestHandler<RestoreListCommand>
    {
        private readonly IArchivedTodoListRepository _lists;
        private readonly IUnitOfWork _uow;

        public RestoreListHandler(IArchivedTodoListRepository lists, IUnitOfWork uow)
        {
            _lists = lists;
            _uow = uow;
        }

        protected override async Task Handle(RestoreListCommand request, CancellationToken cancellationToken)
        {
            var list = await _lists.FindByIdAsync(request.ListId);
            
            if (list is null) return;

            list.Restore();

            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}