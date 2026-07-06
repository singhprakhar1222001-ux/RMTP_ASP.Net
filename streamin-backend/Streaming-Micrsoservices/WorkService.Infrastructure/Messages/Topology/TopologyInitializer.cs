using Contracts.WorkService.Topology;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkService.Infrastructure.Messages.Connection;

namespace WorkService.Infrastructure.Messages.Topology
{
    public class TopologyInitializer : ITopologyInitializer
    {
        private readonly IConnectionManager _connectionManager;
        public TopologyInitializer(IConnectionManager connectionManager)
        {
            this._connectionManager = connectionManager;
        }
        public async Task Initialize()
        {
            //initialize queues and bind

            using var connection= await _connectionManager.GetConnection();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: Topology.QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false
                );
            Dictionary<string, object?> header = new();

            await channel.QueueDeclareAsync(
                queue: Topology.DLQ,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: header
                );
            await channel.QueueBindAsync( //left empty will get name from common project
                 queue:Topology.QueueName,
                 exchange:Topology.ExchangeName,
                 routingKey:Topology.routingKey
                );

            //initialize the exchange which will have the producer algorithm too
            await channel.ExchangeDeclareAsync(
                exchange: WorkExchangeTopology.WorkExchangeName,
                type: ExchangeType.Topic

                );
        }
    }
}
