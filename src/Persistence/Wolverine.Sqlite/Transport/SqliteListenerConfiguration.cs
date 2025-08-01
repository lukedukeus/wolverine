using Wolverine.Configuration;

namespace Wolverine.Sqlite.Transport;

public class SqliteListenerConfiguration : ListenerConfiguration<SqliteListenerConfiguration, SqliteQueue>
{
    public SqliteListenerConfiguration(SqliteQueue endpoint) : base(endpoint)
    {
    }

    public SqliteListenerConfiguration(Func<SqliteQueue> source) : base(source)
    {
    }
}