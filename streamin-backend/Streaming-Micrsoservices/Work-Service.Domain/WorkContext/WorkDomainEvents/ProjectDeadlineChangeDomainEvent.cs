using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using Work_Service.Domain.Abstraction;

namespace Work_Service.Domain.WorkContext.WorkDomainEvents
{
    public class WorkDeadlineChangeDomainEvent(
        DateOnly oldDate,DateOnly NewDate,Guid workId,Guid ProjectId,string Name
        ):DomainEvents
    {
        public string field = "Deadline";
        public DateOnly OldDate { get; private set; } = oldDate;
        public DateOnly NewDate { get; private set; }= NewDate;
        public Guid workId { get; private set; } = workId;
        public Guid ProjectId { get; private set; } = ProjectId;
        public string Name { get; private set; } = Name;
    }
}
