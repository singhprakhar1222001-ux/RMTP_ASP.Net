using RabbitMQ.Client;

namespace Audit_Service.API.Infrastructure.Messaging.Connection
{
    public interface IConnectionManager
    {
        public Task<IConnection> GetConnection();

    }
}
