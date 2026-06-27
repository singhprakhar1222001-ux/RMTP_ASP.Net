using SearchService.API.Infrastructure.Projections.Models;
using System.Collections.Concurrent;

namespace SearchService.API.Infrastructure.Cache
{
    public sealed class ProjectCache
    {
        private readonly ConcurrentDictionary<Guid, ProjectProjection> _cache = new();

        public bool GetValue(Guid projectId, out ProjectProjection? projection)
        {
            
            bool res= _cache.TryGetValue(projectId,out projection);
            return res;
        }

        public void Insert(Guid projectId, ProjectProjection projection) {
            _cache.TryAdd(projectId, projection);
        }
        public bool Remove(Guid projectId) {
            return _cache.TryRemove(projectId, out _);
        }

    }
}
