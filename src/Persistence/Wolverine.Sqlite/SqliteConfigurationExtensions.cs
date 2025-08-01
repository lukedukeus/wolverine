using JasperFx.Core.Reflection;

using Wolverine.Configuration;
using Wolverine.Sqlite.Transport;

namespace Wolverine.Sqlite;

public static class SqliteConfigurationExtensions
{
    /// <summary>
    ///     Register sqlite backed message persistence to a known connection string
    /// </summary>
    /// <param name="settings"></param>
    /// <param name="connectionString"></param>
    public static ISqliteBackedPersistence PersistMessagesWithSqlite(this WolverineOptions options, string connectionString)
    {
        var extension = new SqliteBackedPersistence
        {
            ConnectionString = connectionString
        };

        options.Include(extension);

        return extension;
    }

    /// <summary>
    ///     Quick access to the Rabbit MQ Transport within this application.
    ///     This is for advanced usage
    /// </summary>
    /// <param name="endpoints"></param>
    /// <returns></returns>
    internal static SqliteTransport SqliteTransport(this WolverineOptions endpoints)
    {
        var transports = endpoints.As<WolverineOptions>().Transports;

        try
        {
            return transports.OfType<SqliteTransport>().Single();
        }
        catch (Exception)
        {
            throw new InvalidOperationException("The Sqlite transport is not registered in this system");
        }
    }

    /// <summary>
    /// Listen for incoming messages at the designated Sqlite queue by name
    /// </summary>
    /// <param name="endpoints"></param>
    /// <param name="queueName"></param>
    /// <returns></returns>
    public static SqliteListenerConfiguration ListenToSqliteQueue(this WolverineOptions endpoints, string queueName)
    {
        var transport = endpoints.SqliteTransport();
        var corrected = transport.MaybeCorrectName(queueName);
        var queue = transport.Queues[corrected];
        queue.EndpointName = queueName;
        queue.IsListener = true;

        return new SqliteListenerConfiguration(queue);
    }

    /// <summary>
    ///     Publish matching messages straight to a Sqlite queue using the queue name
    /// </summary>
    /// <param name="publishing"></param>
    /// <param name="queueName"></param>
    /// <returns></returns>
    public static SqliteSubscriberConfiguration ToSqliteQueue(this IPublishToExpression publishing,
        string queueName)
    {
        var transports = publishing.As<PublishingExpression>().Parent.Transports;
        var transport = transports.OfType<SqliteTransport>().Single();

        var corrected = transport.MaybeCorrectName(queueName);
        var queue = transport.Queues[corrected];
        queue.EndpointName = queueName;

        // This is necessary unfortunately to hook up the subscription rules
        publishing.To(queue.Uri);

        return new SqliteSubscriberConfiguration(queue);
    }
}