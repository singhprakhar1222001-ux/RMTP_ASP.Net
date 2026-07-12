using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using Work_Service.Domain.Abstraction;
using WorkService.Persistance.Outbox;

namespace WorkService.Persistance.Interceptor
{
    public class EventInterceptor: SaveChangesInterceptor
    {
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {

            var context = eventData.Context;
            var debugView = context.ChangeTracker.DebugView.LongView;
            Console.WriteLine(debugView);
            var event_entity = context.ChangeTracker.Entries<Entity>();
            var all_entity = context.ChangeTracker.Entries();
            var events = context.ChangeTracker
                .Entries<Entity>()
                .Select(e => e.Entity)
                .SelectMany(entity =>
                {
                    var DomainEvents = entity.getEvents();
                    entity.ClearContext();
                    return DomainEvents;
                }
                );
            //only because efcore has access to scope that initialized it, usually we cannot access service from a dependency this way and we shouldn't
            var OutboxMessages = events?.Select(
                e =>
                    new OutboxMessage
                    {
                        Id = Guid.NewGuid(),
                        Type = e.GetType().ToString(),
                        Message = JsonConvert.SerializeObject(e),
                        IsError = false,
                        IsProcessed = false,
                        OccuredOn = DateTime.UtcNow
                    }
                ).ToList();
            context?.AddRange(OutboxMessages);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
