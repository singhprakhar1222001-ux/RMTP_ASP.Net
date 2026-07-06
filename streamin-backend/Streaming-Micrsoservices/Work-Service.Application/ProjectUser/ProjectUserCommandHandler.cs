using Contracts.WorkService.Events;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using Work_Service.Application.Abstractions;
using Work_Service.Domain.ProjectContext;
using ProjectUserClass= Work_Service.Domain.ProjectUser.ProjectUser;

namespace Work_Service.Application.ProjectUser
{
    public record ProjectUserCommand(
        Guid Id,
        string Name,
        Role role
        ) : IRequest
    { }
    public class ProjectUserCommandHandler : IRequestHandler<ProjectUserCommand>
    {
        private readonly IUnitofWork<ProjectUserClass> _unitofWork;
        public ProjectUserCommandHandler(IUnitofWork<ProjectUserClass> unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task Handle(ProjectUserCommand request, CancellationToken cancellationToken)
        {
            var ProjectUser = ProjectUserClass.CreateProjectUser(
                id: Guid.NewGuid(),
                Name: request.Name,
                role: Role.Employee
                );
            var ProjectUserCreatedEvent = new ProjectUserCreatedEvent(
                Id: ProjectUser.Id,
                EventId: Guid.NewGuid(),
                Name: ProjectUser.Name,
                OccuredOn: DateTime.UtcNow
                );
            ProjectUser.AddEvent(ProjectUserCreatedEvent );
            _unitofWork.Add(ProjectUser);
            await _unitofWork.SaveChangesAsync();
            return;
        }
    }
}
