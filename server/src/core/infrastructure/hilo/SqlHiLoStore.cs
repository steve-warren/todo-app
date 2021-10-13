using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace WarrenSoftware.TodoApp.Core.Infrastructure
{
    public class SqlHiLoStore : IHiLoStore
    {
        private readonly SqlConnection _connection;

        public SqlHiLoStore(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> NextLowAsync(CancellationToken cancellationToken)
        {
            using var command = new SqlCommand(@$"SELECT NEXT VALUE FOR HiLoSequence", _connection);

            await _connection.OpenAsync(cancellationToken);
            var low = (int) await command.ExecuteScalarAsync(cancellationToken);

            return low;
        }
    }
}