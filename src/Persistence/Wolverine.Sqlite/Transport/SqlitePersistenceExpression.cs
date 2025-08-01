using Wolverine.Configuration;
using Wolverine.Transports;

namespace Wolverine.Sqlite.Transport;

public class SqlitePersistenceExpression : BrokerExpression<SqliteTransport, SqliteQueue, SqliteQueue, SqliteListenerConfiguration, SqliteSubscriberConfiguration, SqlitePersistenceExpression>
{
    private readonly WolverineOptions _options;

    public SqlitePersistenceExpression(SqliteTransport transport, WolverineOptions options) : base(transport, options)
    {
        _options = options;
    }

    protected override SqliteListenerConfiguration createListenerExpression(SqliteQueue listenerEndpoint)
    {
        return new SqliteListenerConfiguration(listenerEndpoint);
    }

    protected override SqliteSubscriberConfiguration createSubscriberExpression(SqliteQueue subscriberEndpoint)
    {
        return new SqliteSubscriberConfiguration(subscriberEndpoint);
    }

    /// <summary>
    /// Disable inbox and outbox usage on all Sqlite Transport endpoints
    /// </summary>
    /// <returns></returns>
    public SqlitePersistenceExpression DisableInboxAndOutboxOnAll()
    {
        var policy = new LambdaEndpointPolicy<SqliteQueue>((e, _) =>
        {
            if (e.Role == EndpointRole.System)
            {
                return;
            }

            e.Mode = EndpointMode.BufferedInMemory;
        });

        _options.Policies.Add(policy);
        return this;
    }
}