using Audit_Service.API.Infrastructure.Messaging.Connection;
using Contracts.WorkService.Topology;
using RabbitMQ.Client;

namespace Audit_Service.API.Infrastructure.Messaging.Topology
{
    public class TopologyInitializer : ITopologyInitializer
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
                exchange: Topology.retryExchange30s,
                type: ExchangeType.Topic
                );
            await channel.ExchangeDeclareAsync(
                exchange: Topology.retryExchange60s,
                type: ExchangeType.Topic
                );
            await channel.ExchangeDeclareAsync(
                exchange: Topology.retryExchange90s,
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

            var retryArgs30s = new Dictionary<string, object>
            {
                ["x-message-ttl"] = 30000,
                ["x-dead-letter-exchange"] = WorkExchangeTopology.WorkExchangeName,

            };
            await channel.QueueDeclareAsync(
                queue: Topology.retryQueue30s,
                durable: true,
                exclusive: true,
                autoDelete: false,
                arguments: retryArgs30s
                );

            await channel.QueueBindAsync(
                queue: Topology.retryQueue30s,
                exchange: Topology.retryExchange30s,
                routingKey: Topology.routingKey//this is a binding key, used to preserve the original key
                );

            var retryArgs60s = new Dictionary<string, object>
            {
                ["x-message-ttl"] = 60000,
                ["x-dead-letter-exchange"] = WorkExchangeTopology.WorkExchangeName,

            };
            await channel.QueueDeclareAsync(
                queue: Topology.retryQueue60s,
                durable: true,
                exclusive: true,
                autoDelete: false,
                arguments: retryArgs60s
                );

            await channel.QueueBindAsync(
                queue: Topology.retryQueue60s,
                exchange: Topology.retryExchange60s,
                routingKey: Topology.routingKey//this is a binding key, used to preserve the original key
                );

            var retryArgs90s = new Dictionary<string, object>
            {
                ["x-message-ttl"] = 90000,
                ["x-dead-letter-exchange"] = WorkExchangeTopology.WorkExchangeName,

            };
            await channel.QueueDeclareAsync(
                queue: Topology.retryQueue90s,
                durable: true,
                exclusive: true,
                autoDelete: false,
                arguments: retryArgs90s
                );

            await channel.QueueBindAsync(
                queue: Topology.retryQueue90s,
                exchange: Topology.retryExchange90s,
                routingKey: Topology.routingKey //this is a binding key, used to preserve the original key
                );
        }
    }
}
