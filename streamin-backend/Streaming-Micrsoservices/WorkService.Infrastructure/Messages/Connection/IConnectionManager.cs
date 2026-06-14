using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkService.Infrastructure.Messages.Connection
{
    public interface IConnectionManager
    {
        public Task<IConnection> GetConnection();
    }
}
