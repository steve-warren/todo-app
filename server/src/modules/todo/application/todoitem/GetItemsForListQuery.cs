using MediatR;
using Microsoft.Data.SqlClient;
using WarrenSoftware.TodoApp.Core.Domain;
using WarrenSoftware.TodoApp.Core.Infrastructure;

namespace WarrenSoftware.TodoApp.Modules.Todo;

public class GetItemsForListQuery : IRequest
{
    public int OwnerId { get; init; }
    public int ListId { get; init; }
    public Stream OutputStream { get; init; } = Stream.Null;
}

public class GetItemsForListHandler : IRequestHandler<GetItemsForListQuery>
{
    private readonly SqlConnection _connection;

    public GetItemsForListHandler(SqlConnection connection)
    {
        _connection = connection;
    }

    public async Task Handle(GetItemsForListQuery request, CancellationToken cancellationToken)
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
                        AND
                            ArchiveState = @ArchiveState
                FOR JSON AUTO
            ", _connection);

        command.Parameters.AddWithValue("@OwnerId", request.OwnerId);
        command.Parameters.AddWithValue("@ListId", request.ListId);
        command.Parameters.AddWithValue("@ArchiveState", ArchiveState.NotArchived.Name);

        await _connection.OpenAsync(cancellationToken);
        await command.StreamUtf8TextAsync(request.OutputStream, cancellationToken);
    }
}
