using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Data.SqlClient;
using WarrenSoftware.TodoApp.Core.Infrastructure;

namespace WarrenSoftware.TodoApp.Modules.Todo
{
    public class GetItemsQuery : IRequest
    {
        public int OwnerId { get; init; }
        public int ListId { get; init; }
        public Stream OutputStream { get; init; } = Stream.Null;
    }

    public class GetItemsHandler : IRequestHandler<GetItemsQuery>
    {
        private readonly SqlConnection _connection;

        public GetItemsHandler(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<Unit> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            using var command = new SqlCommand(@"
                SELECT
                    Id,
                    Name,
                    Notes,
                    Reminder,
                    Priority,
                    State
                FROM
                    TodoItems
                WHERE
                    OwnerId = @OwnerId
                    AND
                    ListId = @ListId
            ", _connection);

            command.Parameters.AddWithValue("@OwnerId", request.OwnerId);
            command.Parameters.AddWithValue("@ListId", request.ListId);

            await _connection.OpenAsync(cancellationToken);
            await command.StreamUtf8TextAsync(request.OutputStream, cancellationToken);

            return Unit.Value;
        }
    }
}