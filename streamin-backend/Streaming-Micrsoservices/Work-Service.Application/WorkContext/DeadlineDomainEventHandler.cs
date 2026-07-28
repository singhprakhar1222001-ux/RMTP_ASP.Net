using Contracts.WorkService.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Work_Service.Application.Abstractions;
using Work_Service.Domain.WorkContext;
using Work_Service.Domain.WorkContext.WorkDomainEvents;

namespace Work_Service.Application.WorkContext
{
    internal class DeadlineDomainEventHandler:INotificationHandler<WorkDeadlineChangeDomainEvent>
    {
        private readonly IUnitofWork<Workitem> _unitofwork;
        public DeadlineDomainEventHandler(IUnitofWork<Workitem> unitofWork)
        {
            _unitofwork = unitofWork;
        }
        public async Task Handle(WorkDeadlineChangeDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            //add outbox

            ChangeEvent changeEvent = new ChangeEvent(
                EventId: Guid.NewGuid(),
                occuredon: DateTime.UtcNow,
                workId: domainEvent.workId,
                WorkName: domainEvent.Name,
                ProjectId: domainEvent.ProjectId,
                ActorId: Guid.NewGuid(),
                AuditPayload: new AuditPayload(
                    comment:null,
                    change: new ChangeAudit(domainEvent.field, oldvalue: domainEvent.OldDate.ToString(), newvalue: domainEvent.NewDate.ToString())
                    )
                );
            _unitofwork.AddOutbox( changeEvent );
        }
    }
}
