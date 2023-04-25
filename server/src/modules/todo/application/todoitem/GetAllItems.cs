using MediatR;
using Microsoft.Data.SqlClient;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Core.Infrastructure;

namespace WarrenSoftware.TodoApp.Modules.Todo
{
    public class GetAllItemsQuery : IRequest
    {
        public int OwnerId { get; init; }
        public Stream OutputStream { get; init; } = Stream.Null;
    }

    public class GetAllItemsHandler : IRequestHandler<GetAllItemsQuery>
    {
        private readonly SqlConnection _connection;

        public GetAllItemsHandler(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
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
                    ArchiveState = @ArchiveState
                FOR JSON AUTO
            ", _connection);

            command.Parameters.AddWithValue("@OwnerId", request.OwnerId);
            command.Parameters.AddWithValue("@ArchiveState", ArchiveState.NotArchived.Name);

            await _connection.OpenAsync(cancellationToken);
            await command.StreamUtf8TextAsync(request.OutputStream, cancellationToken);
        }
    }
}