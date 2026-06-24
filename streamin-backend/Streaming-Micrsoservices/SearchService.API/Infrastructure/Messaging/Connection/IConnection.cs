using RabbitMQ.Client;

namespace SearchService.API.Infrastructure.Messaging.Connection
{
    public interface IConnectionManager
    {
        
            public Task<IConnection> GetConnection();
        
    }
}
