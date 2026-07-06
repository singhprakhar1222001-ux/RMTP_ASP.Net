using Contracts;
using Contracts.WorkService.RoutingEventDirectory;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SearchService.API.Infrastructure.Messaging.Connection;
using SearchService.API.Infrastructure.Messaging.Topology;
using System.Reflection;
using System.Text;

namespace SearchService.API.Infrastructure.Messaging.BackgroundJobs
{
    public class ConsumerJob:BackgroundService
    {
        private IConnectionManager connectionManager;
        private IServiceScopeFactory scopeFactory;
        public ConsumerJob(IConnectionManager connectionManager, IServiceScopeFactory scopeFactory)
        {
            this.connectionManager = connectionManager;
            this.scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connection = await connectionManager.GetConnection();
            var channel = await connection.CreateChannelAsync();

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (sender, EventArgs) =>
            {
                try
                {
                    var scope = scopeFactory.CreateScope();
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    string routing_key = EventArgs.RoutingKey;
                    Type type = RoutingEventDirectory.GetTypeInference(routing_key);
                    byte[] body = EventArgs.Body.ToArray();
                    string body_string = Encoding.UTF8.GetString(body);
                    var responseBody = JsonConvert.DeserializeObject(body_string, type);
                    await mediator.Send(responseBody);
                    await ((AsyncEventingBasicConsumer)sender).Channel.BasicAckAsync(EventArgs.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            };
            await channel.BasicConsumeAsync(Topology.Topology.EventQueue, autoAck: false, consumer);

            await Task.Delay(Timeout.Infinite, stoppingToken);


        }
    }
}

