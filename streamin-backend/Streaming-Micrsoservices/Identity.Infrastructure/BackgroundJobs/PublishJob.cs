using Identity.Infrastructure.Messages.Topology;
using Identity.Infrastructure.Outbox;
using Identity.Infrastructure.Persistance;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Identity.Infrastructure.BackgroundJobs
{
    public class PublishJob : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
       
        public PublishJob(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    Console.WriteLine("PublishStarted");
                    using var scope = _scopeFactory.CreateScope();
                    var dbcontext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
                    var topologyInitializor = scope.ServiceProvider.GetRequiredService<IToplogyInitializor>();
                    List<OutboxMessage> outboxMessages = dbcontext.OutboxMessages.Where(x => x.IsProcessed == false).Take(20).ToList();
                    var channel = topologyInitializor.GetChannel();
                    foreach (var outboxMessage in outboxMessages)
                    {
                        var message = outboxMessage.Message;
                        byte[] messageByte = UTF8Encoding.UTF8.GetBytes(message);
                        await channel.BasicPublishAsync(
                            exchange: Topology.ExchangeName,
                            routingKey: Topology.routingKey,
                            body: messageByte,
                            mandatory: true,
                            cancellationToken: stoppingToken
                            );
                        outboxMessage.IsProcessed = true;
                        await dbcontext.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
