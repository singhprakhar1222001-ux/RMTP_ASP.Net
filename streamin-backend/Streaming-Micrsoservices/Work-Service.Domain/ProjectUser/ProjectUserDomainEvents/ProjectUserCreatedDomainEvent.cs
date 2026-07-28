using System;
using System.Collections.Generic;
using System.Text;
using Work_Service.Domain.Abstraction;

namespace Work_Service.Domain.ProjectUser.ProjectUserDomainEvents
{
    public class ProjectUserCreatedDomainEvent(
        Guid Id,
        Guid EventId,
        string Name,
        DateTime OccuredOn,
        Guid ProjectId
        ):DomainEvents
    {
        public Guid Id { get; private set; } = Id;
        public Guid ProjectId { get; private set; }= ProjectId;
        public Guid EventId { get; private set; } = EventId;
        public string Name { get; private set; } = Name;
        public DateTime OccuredOn { get; private set; } = OccuredOn;
    }
}
