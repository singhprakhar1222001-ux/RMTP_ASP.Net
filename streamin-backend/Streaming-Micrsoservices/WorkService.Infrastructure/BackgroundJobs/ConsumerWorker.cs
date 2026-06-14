
using Contracts.Identity.Implementations;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Work_Service.Domain.Projections;
using WorkService.Infrastructure.Messages.Connection;
using Work_Service.Application.Projections;
using WorkService.Infrastructure.Messages.Topology;
using RabbitMQ.Client;

namespace WorkService.Infrastructure.BackgroundJobs
{
    public class ConsumerWorker: BackgroundService
    {
        private readonly IConnectionManager _connection;
        private readonly IServiceScopeFactory _scopeFactory;

        public ConsumerWorker(IConnectionManager connectionManager, IServiceScopeFactory scopeFactory)
        {
            _connection = connectionManager;
            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                try
                {
                    using var connection = await _connection.GetConnection();
                    using var channel = await connection.CreateChannelAsync();

                    var consumer = new AsyncEventingBasicConsumer(channel);
                    consumer.ReceivedAsync += async (sender, EventArgs) =>
                    {
                        Console.WriteLine($"Event arg redelivered: {EventArgs.Redelivered}");
                        byte[] message = EventArgs.Body.ToArray();
                        string messageString = Encoding.UTF8.GetString(message);
                        var messageBody = JsonConvert.DeserializeObject<UserCreatedIntegrationEvent>(messageString);
                        if (messageBody != null)
                        {
                            UserCreateRequestCommand userCreateRequestCommand = new UserCreateRequestCommand(messageBody.UserName, messageBody.userId, messageBody.Membership);
                            await _mediator.Send(userCreateRequestCommand);
                        }

                        await ((AsyncEventingBasicConsumer)sender).Channel.BasicAckAsync(EventArgs.DeliveryTag, multiple: false);
                        //here we will implement idempotent read too.
                    };
                    await channel.BasicConsumeAsync(Topology.QueueName, autoAck: false, consumer);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                await Task.Delay(10000, stoppingToken);
            }
        }
    }

   
}
