using Contracts.WorkService.Topology;
using RabbitMQ.Client;
using SearchService.API.Infrastructure.Messaging.Connection;

namespace SearchService.API.Infrastructure.Messaging.Topology
{
    public class TopologyInitializer:ITopologyInitializer
    {
        private readonly IConnectionManager _connectionManager;
        public TopologyInitializer(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public async Task Initialize()
        {
            var connection = await _connectionManager.GetConnection();
            var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(
                exchange: Topology.retryExchange,
                type: ExchangeType.Topic
                );

            await channel.QueueDeclareAsync(
                queue: Topology.EventQueue,
                durable: true,
                exclusive: true,
                autoDelete: false
                );

            await channel.QueueBindAsync(
                queue: Topology.EventQueue,
                exchange: WorkExchangeTopology.WorkExchangeName,
                routingKey: Topology.routingKey
                );

            var retryArgs = new Dictionary<string, object>
            {
                ["x-message-ttl"] = 30000,
                ["x-dead-letter-exchange"] = WorkExchangeTopology.WorkExchangeName,
                
            };
            await channel.QueueDeclareAsync(
                queue: Topology.RetryQueue,
                durable: true,
                exclusive: true,
                autoDelete: false,
                arguments: retryArgs
                );

            await channel.QueueBindAsync(
                queue: Topology.RetryQueue,
                exchange: Topology.retryExchange,
                routingKey: Topology.routingKey//this is a binding key, used to preserve the original key
                );
        }
    }
}
