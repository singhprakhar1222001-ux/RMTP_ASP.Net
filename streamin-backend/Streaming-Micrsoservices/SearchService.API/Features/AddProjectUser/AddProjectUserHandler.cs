using Contracts.WorkService.Events;
using MediatR;
using SearchService.API.Infrastructure.Cache;
using SearchService.API.Infrastructure.Projections;
using SearchService.API.Infrastructure.Projections.Models;

namespace SearchService.API.Features.AddProjectUser
{
    public record CreateProjectRequest : ProjectUserCreatedEvent, IRequest
    {
        public CreateProjectRequest(Guid Id, Guid EventId, string Name, DateTime OccuredOn) : base(Id, EventId, Name, OccuredOn)
        {
        }
    }
    public class AddProjectUserHandler : IRequestHandler<CreateProjectRequest>
    {
        private readonly AppDbContext _context;
        private ProjectUserCache cache;
        public AddProjectUserHandler(ProjectUserCache cache, AppDbContext context)
        {
            this.cache = cache;
            this._context = context;
        }
        public async Task Handle(CreateProjectRequest request, CancellationToken cancellationToken)
        {
            //just add to your cache and database in a single transaction
            var projection = new ProjectUserProjection();
            projection.UserId = request.Id;
            projection.UserName = request.Name;

            cache.Insert(request.Id, projection);

            _context.ProjectUserProjections.Add(projection);
            await _context.SaveChangesAsync();
            
        }
    }
    
}
