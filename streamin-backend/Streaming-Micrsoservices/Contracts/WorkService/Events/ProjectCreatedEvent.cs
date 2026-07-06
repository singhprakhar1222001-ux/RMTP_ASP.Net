using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.WorkService.Events
{
    public record ProjectCreatedEvent(
        Guid Id,
        Guid EventId,
        string ProjectName,
        DateTime OccuredOn
        ):IIntegreationEvent
    {
        public Guid Id { get; private set; } = Id;
        public string ProjectName { get; private set; }= ProjectName;
        public Guid EventId { get; private set; }= EventId;
        public DateTime OccuredOn { get; private set; } = OccuredOn;
    }
}
