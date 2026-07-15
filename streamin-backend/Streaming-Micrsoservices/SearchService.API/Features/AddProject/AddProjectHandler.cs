using Contracts.WorkService.Events;
using MediatR;
using SearchService.API.Infrastructure.Cache;
using SearchService.API.Infrastructure.Projections;
using SearchService.API.Infrastructure.Projections.Models;

namespace SearchService.API.Features.AddProject
{
    //public record CreateProjectRequest : ProjectCreatedEvent, IRequest
    //{
    //    public CreateProjectRequest(Guid Id, Guid EventId, string ProjectName, DateTime OccuredOn) : base(Id, EventId, ProjectName, OccuredOn)
    //    {
    //    }
    //}
    public class AddProjectHandler : INotificationHandler<ProjectCreatedEvent>
    {
        private readonly AppDbContext _context;
        private ProjectCache cache;
        public AddProjectHandler(ProjectCache cache,AppDbContext context)
        {
           this.cache=cache;
           this._context=context;
        }
        public async Task Handle(ProjectCreatedEvent request, CancellationToken cancellationToken)
        {
            //just add to your cache and database in a single transaction
            ProjectProjection projection = new ProjectProjection();
            projection.ProjectId = request.Id;
            projection.ProjectName = request.ProjectName;

            cache.Insert(request.Id, projection);

            _context.ProjectProjections.Add(projection);
        }

        
    }
}
