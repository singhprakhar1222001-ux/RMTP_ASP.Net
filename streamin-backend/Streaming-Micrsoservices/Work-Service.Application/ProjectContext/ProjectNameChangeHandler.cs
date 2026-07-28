using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Work_Service.Application.Abstractions;
using Work_Service.Domain.ProjectContext;

namespace Work_Service.Application.ProjectContext
{
    public record ProjectNameChangeCommand(
        Guid ProjectId,
        string Name
        ) : IRequest
    {

    }
    public class ProjectNameChangeHandler : IRequestHandler<ProjectNameChangeCommand>
    {
        private readonly IUnitofWork<ProjectBase> _unitofWork;
        public ProjectNameChangeHandler(IUnitofWork<ProjectBase> unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public async Task Handle(ProjectNameChangeCommand command, CancellationToken ct)
        {
            ProjectBase project = await _unitofWork.GetEntity(command.ProjectId);
            project.ChangeName(command.Name);
            return;
        }
    }
}
