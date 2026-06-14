using Identity.Application.Abstractions;
using Identity.Infrastructure.Outbox;
using Identity.SharedKernel;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace Identity.Infrastructure.Interceptor
{
    public class EventInterceptor : SaveChangesInterceptor
    {
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            
            var context = eventData.Context;
            var eventContext = context?.GetService<IEventContext>();//only because efcore has access to scope that initialized it, usually we cannot access service from a dependency this way and we shouldn't
            var events = eventContext?.GetEvents();
            var OutboxMessages=events?.Select(
                e=>
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
            eventContext?.DeleteEvent();
            
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
