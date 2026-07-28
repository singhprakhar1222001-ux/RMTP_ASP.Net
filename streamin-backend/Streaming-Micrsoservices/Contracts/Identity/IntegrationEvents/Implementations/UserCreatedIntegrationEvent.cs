using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Identity.IntegrationEvents.Implementations
{




    public record 
        UserCreatedIntegrationEvent (
        Guid userId,
        string UserName, 
        string Membership
        ): IIntegreationEvent
    {
        public Guid EventId { get; init; }=Guid.NewGuid();

        public Guid UserId { get; init; } = userId;

        public DateTime OccuredOn { get; init; } = DateTime.UtcNow;
        public string UserName { get; init; }= UserName;
        public string Membership { get; init; }= Membership;
    }
}
