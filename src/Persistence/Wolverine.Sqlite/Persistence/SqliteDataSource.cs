using System.Data.Common;

using Microsoft.Data.Sqlite;

namespace Wolverine.Sqlite.Persistence
{
    public class SqliteDataSource(string connectionString) : DbDataSource
    {
        public override string ConnectionString => connectionString;

        protected override DbConnection CreateDbConnection()
        {
            return new SqliteConnection(ConnectionString);
        }
    }
}
