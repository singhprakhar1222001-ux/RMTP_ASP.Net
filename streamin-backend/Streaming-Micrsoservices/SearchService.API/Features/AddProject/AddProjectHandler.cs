using Contracts.WorkService.Events;
using MediatR;
using SearchService.API.Infrastructure.Cache;
using SearchService.API.Infrastructure.Projections;
using SearchService.API.Infrastructure.Projections.Models;

namespace SearchService.API.Features.AddProject
{
    public class CreateProjectRequest:ProjectCreatedEvent, IRequest<Unit> { }
    public class AddProjectHandler:IRequestHandler<CreateProjectRequest>
    {
        private readonly AppDbContext _context;
        private ProjectCache cache;
        public AddProjectHandler(ProjectCache cache,AppDbContext context)
        {
           this.cache=cache;
            this._context=context;
        }
        public async Task<Unit> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
        {
            //just add to your cache and database in a single transaction
            ProjectProjection projection = new ProjectProjection();
            projection.ProjectId = request.Id;
            projection.ProjectName = request.ProjectName;

            cache.Insert(request.Id, projection);

            _context.ProjectProjections.Add(projection);
            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
