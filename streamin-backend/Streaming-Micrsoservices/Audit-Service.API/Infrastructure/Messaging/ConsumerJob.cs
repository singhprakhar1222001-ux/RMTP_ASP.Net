using Audit_Service.API.Infrastructure.BatchService;
using Audit_Service.API.Infrastructure.Messaging.Connection;
using Audit_Service.API.Infrastructure.Messaging.Topology;
using Microsoft.EntityFrameworkCore.Storage.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;
using Contracts.WorkService.Events;
using Audit_Service.API.Infrastructure.Persistence;

namespace Audit_Service.API.Infrastructure.Messaging
{
    public class ConsumerJob(
        IConnectionManager connectionManager,
        BatchChannel batchChannel,
        IServiceScopeFactory scopeFactory
        ) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connection = await connectionManager.GetConnection();
            var channel = await connection.CreateChannelAsync();

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (sender, eventArgs)=>
            {
                try
                {
                    var bodyArray = eventArgs.Body.ToArray();
                    string body = UTF8Encoding.UTF8.GetString(bodyArray);
                    var EventObject = JsonConvert.DeserializeObject<ChangeEvent>(body);
                    AuditEvent auditEvent = new AuditEvent
                    {
                        EventId = EventObject.EventId,
                        ActorId = EventObject.ActorId,
                        WorkId = EventObject.workId.Value,
                        Properties = JsonConvert.SerializeObject(EventObject.AuditPayload),
                        OccuredOn = EventObject.OccuredOn
                    };
                    await batchChannel.TryWriteAsync(auditEvent, stoppingToken);
                }
                catch (Exception ex) {
                    throw ex;
                }
            };
            await channel.BasicConsumeAsync(Topology.Topology.EventQueue, autoAck: false, consumer);
            await Task.Delay(Timeout.Infinite);
        }
    }
}
