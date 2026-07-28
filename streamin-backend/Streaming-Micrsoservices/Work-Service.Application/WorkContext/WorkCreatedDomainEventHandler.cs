using Contracts.WorkService.Consts;
using Contracts.WorkService.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using Work_Service.Application.Abstractions;
using Work_Service.Domain.WorkContext;


namespace Work_Service.Application.WorkContext
{
    //public class WorkCreatedDomainEventHandler : INotificationHandler<WorkCreatedDomainEvent>
    //{
    //    private readonly IUnitofWork<Workitem> unitofWork;
    //    public WorkCreatedDomainEventHandler(IUnitofWork<Workitem> unitofWork)
    //    {
    //        this.unitofWork = unitofWork;
    //    }

    //    public async Task Handle(WorkCreatedDomainEvent workEvent, CancellationToken ct)
    //    {
    //        WorkCreatedEvent IntegrationEvent = new WorkCreatedEvent(
    //            eventID: workEvent.EventId,
    //            Id: workEvent.Id,
    //            name: workEvent.name,
    //            description: workEvent.description,
    //            comment:new List<CommentEventProperty>(),
    //            ProjectId:workEvent.ProjectId,
    //            assignedId:workEvent.assignedId,
    //            managerId:workEvent.managerId,
    //            assignmentDate:workEvent.assignmentDate,
    //            deadline:workEvent.deadline
    //            );
    //        //add outbox here
    //        unitofWork.AddOutbox(IntegrationEvent);
            
    //    }
    //}

    
}
