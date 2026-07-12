using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.WorkService.Events
{
    public record ProjectUserCreatedEvent(
        Guid Id,
        Guid EventId,
        string Name,
        DateTime OccuredOn
        ):IIntegreationEvent,INotification
    {
        public Guid Id { get; private set; } = Id;
        public Guid EventId { get; private set; } = EventId;
        public string Name { get; private set; } = Name;
        public DateTime OccuredOn { get; private set; } = OccuredOn;
        
    }
}
