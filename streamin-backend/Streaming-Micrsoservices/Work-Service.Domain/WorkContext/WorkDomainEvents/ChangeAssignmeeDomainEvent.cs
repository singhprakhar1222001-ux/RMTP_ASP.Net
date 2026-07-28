using System;
using System.Collections.Generic;
using System.Text;
using Work_Service.Domain.Abstraction;

namespace Work_Service.Domain.WorkContext.WorkDomainEvents
{
    public class ChangeAssignmeeDomainEvent:DomainEvents
    {
        public ChangeAssignmeeDomainEvent(Guid newAssignee, Guid oldAssignee,
            Guid WorkId, Guid ProjectId, string Name
            ) {
            this.OldAssignedId = oldAssignee;
            this.NewAssignedId = newAssignee;
        }
        public Guid WorkId { get; private set; }
        public Guid ProjectId { get; private set; }
        public string Name { get; private set; }
        public Guid NewAssignedId { get; private set; }
        public Guid OldAssignedId { get; private set; }

        public string ChangedField = "AssignedPerson";
    }
}
