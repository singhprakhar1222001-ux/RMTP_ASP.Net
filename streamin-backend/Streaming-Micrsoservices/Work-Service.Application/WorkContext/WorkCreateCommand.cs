using Contracts.WorkService.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Work_Service.Application.Abstractions;
using Work_Service.Domain.WorkContext;

namespace Work_Service.Application.WorkContext
{
    public record WorkCreateCommand(
        string Name,
        string Description,
        string Comment,
        Guid ProjectId,
        Guid AssigneeId,
        Guid ManagerId,
        DateOnly Deadline
        ) : IRequest<Unit> { }

    public class WorkCreateCommandHandler : IRequestHandler<WorkCreateCommand, Unit>
    {
        private readonly IUnitofWork<Workitem> _unitofwork;
        public WorkCreateCommandHandler(IUnitofWork<Workitem> unitofWork) {
            _unitofwork = unitofWork;
        }
        public async Task<Unit> Handle(WorkCreateCommand request, CancellationToken cancellationToken)
        {
            Workitem workitem = Workitem.CreateWorkItem(
                Name: request.Name,
                description: request.Description,
                comment: request.Comment,
                ProjectId: request.ProjectId,
                assignedId: request.AssigneeId,
                Deadline: request.Deadline,
                managerId:request.ManagerId
                );
            //add event in outbox
            var CreationEvent = new WorkCreatedEvent(
                eventID: Guid.NewGuid(),
                Id: workitem.Id,
                name:workitem.Name,
                description: workitem.description,
                comment: workitem.comment,
                ProjectId: workitem.ProjectId,
                assignedId: workitem.assignedId,
                deadline: workitem.Deadline,
                managerId: workitem.managerId,
                assignmentDate:workitem.AssignmentDate
                );
            workitem.AddEvent(CreationEvent);
            _unitofwork.Add(workitem);
            
            await _unitofwork.SaveChangesAsync();
            return Unit.Value;
            
        }
    }

}
