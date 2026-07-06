using Contracts.WorkService.Consts;
using Contracts.WorkService.Events;
using MediatR;

namespace SearchService.API.Features.AddIndex
{
    public record WorkIndexCommand : WorkCreatedEvent, IRequest
    {
        public WorkIndexCommand(Guid eventID, Guid Id, string name, string description, List<CommentEventProperty> comment, Guid ProjectId, Guid assignedId, Guid managerId, DateOnly assignmentDate, DateOnly deadline) : base(eventID, Id, name, description, comment, ProjectId, assignedId, managerId, assignmentDate, deadline)
        {
        }
    }
}
