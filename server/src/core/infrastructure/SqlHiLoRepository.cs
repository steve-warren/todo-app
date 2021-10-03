using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace WarrenSoftware.TodoApp.Core.Infrastructure
{
    public class SqlHiLoRepository : IHiLoRepository
    {
        private readonly SqlConnection _connection;
        private readonly string _sequenceName;

        public SqlHiLoRepository(SqlConnection connection, string sequenceName)
        {
            _connection = connection;
            _sequenceName = sequenceName;
        }

        public async Task<int> NextLowAsync(CancellationToken cancellationToken)
        {
            using var command = new SqlCommand(@$"
            SELECT NEXT VALUE FOR {_sequenceName}
            ", _connection);

            await _connection.OpenAsync(cancellationToken);
            var low = (int) await command.ExecuteScalarAsync(cancellationToken);

            return low;
        }
    }
}