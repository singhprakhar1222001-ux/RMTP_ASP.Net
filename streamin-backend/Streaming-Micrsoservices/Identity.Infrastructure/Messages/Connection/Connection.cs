using Identity.Infrastructure.Messages.Configuration;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Messages.Connection
{
    public class Connection : IConnectionCreator
    {
        private IConnection _connection;
        private SemaphoreSlim _semaphore=new (1,1);
        private readonly IConfiguration _configuration;
        public Connection(IConfiguration configuration)
        {
            _connection = null;
            _configuration = configuration;
        }
        public async Task<IConnection> GetConnection()
        {
            if(_connection == null)
            {
                var rabbitmqConnectionString = _configuration.GetConnectionString("rabbitmq");
                try{
                    await _semaphore.WaitAsync();

                    var factory = new ConnectionFactory();
                    factory.Uri = new Uri(rabbitmqConnectionString);
                    _connection = await factory.CreateConnectionAsync();
                    
                }
                finally
                {
                    _semaphore.Release();
                }
            }
            return _connection;
        }
        public async Task<IChannel> GetChannel()
        {
            var channel = await _connection.CreateChannelAsync();
            return channel;
        }
    }
}
