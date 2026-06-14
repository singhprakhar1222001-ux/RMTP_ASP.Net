using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Messages.Connection
{
    public interface IConnectionCreator
    {
        public Task<IConnection> GetConnection();
        public Task<IChannel> GetChannel();
    }
}
