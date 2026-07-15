using Contracts.WorkService.Events;
using MediatR;
using SearchService.API.Infrastructure.Cache;
using SearchService.API.Infrastructure.Projections;
using SearchService.API.Infrastructure.Projections.Models;

namespace SearchService.API.Features.AddProjectUser
{
    
    public class AddProjectUserHandler : INotificationHandler<ProjectUserCreatedEvent>
    {
        private readonly AppDbContext _context;
        private ProjectUserCache cache;
        public AddProjectUserHandler(ProjectUserCache cache, AppDbContext context)
        {
            this.cache = cache;
            this._context = context;
        }
        public async Task Handle(ProjectUserCreatedEvent request, CancellationToken cancellationToken)
        {
            //just add to your cache and database in a single transaction
            var projection = new ProjectUserProjection();
            projection.UserId = request.Id;
            projection.UserName = request.Name;

            cache.Insert(request.Id, projection);

            _context.ProjectUserProjections.Add(projection);
            
        }
    }
    
}
