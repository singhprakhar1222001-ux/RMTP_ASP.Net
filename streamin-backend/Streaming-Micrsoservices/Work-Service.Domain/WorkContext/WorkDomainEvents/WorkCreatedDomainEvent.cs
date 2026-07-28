using Contracts.WorkService.Consts;
using System;
using System.Collections.Generic;
using System.Text;
using Work_Service.Domain.Abstraction;

namespace Work_Service.Domain.WorkContext.WorkDomainEvents
{
    public class WorkCreatedDomainEvent(
         Guid eventID,
         Guid Id,
         string name,
         string description,
         Guid ProjectId,
         Guid assignedId,
         Guid managerId,
         DateOnly assignmentDate,
         DateOnly deadline
        ) :DomainEvents
    {
        public Guid EventId { get; set; } = eventID;
        public DateTime OccuredOn { get; set; } = DateTime.UtcNow;
        public Guid Id { get; set; } = Id;
        public string Name { get; set; } = name;
        public Guid ProjectId { get; private set; } = ProjectId;
        public string description { get; private set; } = description;
        public Guid assignedId { get; private set; } = assignedId;

        public Guid managerId { get; private set; } = managerId;

        public DateOnly AssignmentDate { get; private set; } = assignmentDate;

        public DateOnly Deadline { get; private set; } = deadline;
        public bool IsOverDue { get; private set; } = false;
        public WorkStatus WorkStatus { get; private set; } = WorkStatus.InProgress;
        public int Version { get; private set; }
    }
}
