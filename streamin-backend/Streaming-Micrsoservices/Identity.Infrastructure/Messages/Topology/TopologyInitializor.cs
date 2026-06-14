using Identity.Infrastructure.Messages.Connection;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Messages.Topology
{
    public class TopologyInitializor:IToplogyInitializor
    {
        private readonly IConnectionCreator _connectionCreator;
        private IChannel channel;
        private IConnection connection;
        public TopologyInitializor(IConnectionCreator connectionCreator)
        {
            _connectionCreator=connectionCreator;
        }

        public async Task Initialize()
        {
            //create exchange first;
            connection = await _connectionCreator.GetConnection();
            channel=await _connectionCreator.GetChannel();

            await channel.ExchangeDeclareAsync(
                 exchange: Topology.ExchangeName,
                 type: ExchangeType.Topic,
                 durable: true,
                 autoDelete: true
                );

        }

        public IChannel GetChannel()
        {
            return channel;
        }
    }
}
