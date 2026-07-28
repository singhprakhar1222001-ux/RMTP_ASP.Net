using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using DomainEvents = Work_Service.Domain.Abstraction.DomainEvents;
namespace Work_Service.Domain.ProjectContext.DomainEventss
{
    public class ProjectCreatedDomainEvent(
        Guid ProjectId,
        string Name,
        string Description,
        Guid ProjectHead
                ):DomainEvents
    {
        

        public Guid ProjectId { get; set; } = ProjectId;
        public string Name { get; set; } = Name;
        public string Description { get; set; } = Description;
        public Guid ProjectHead { get; set; }= ProjectHead;

    }
}
