using Contracts.WorkService.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Work_Service.Application.Abstractions;
using Work_Service.Domain.ProjectContext;

namespace Work_Service.Application.ProjectContext
{
    public record ProjectUserCommand(
        string Name,
        string Description,
        Guid ProjectHead
        ) : IRequest { }
    public class ProjectUserCommandHandler : IRequestHandler<ProjectUserCommand>
    {
        private readonly IUnitofWork<ProjectBase> _unitOfWork;
        public ProjectUserCommandHandler(IUnitofWork<ProjectBase> unitofWork)
        {
            _unitOfWork= unitofWork;
        }
        public async Task Handle(ProjectUserCommand request, CancellationToken cancellationToken)
        {
            var projectBase=ProjectBase.CreateProject(Name: request.Name, Description: request.Description, Projecthead: request.ProjectHead);
            var ProjectCreatedEvent = new ProjectCreatedEvent(
                Id: projectBase.Id,
                EventId: Guid.NewGuid(),
                ProjectName: projectBase.Name,
                OccuredOn: DateTime.UtcNow
                );
            projectBase.AddEvent(ProjectCreatedEvent);
            _unitOfWork.Add(projectBase);
            await _unitOfWork.SaveChangesAsync();
            return;
        }
    }
}
