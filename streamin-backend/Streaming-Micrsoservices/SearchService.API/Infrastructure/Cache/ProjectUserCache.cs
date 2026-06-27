using SearchService.API.Infrastructure.Projections.Models;
using System.Collections.Concurrent;

namespace SearchService.API.Infrastructure.Cache
{
    public class ProjectUserCache
    {
        private readonly ConcurrentDictionary<Guid, ProjectUserProjection> _cache = new();

        public bool GetValue(Guid projectId, out ProjectUserProjection? projection)
        {

            bool res = _cache.TryGetValue(projectId, out projection);
            return res;
        }

        public void Insert(Guid projectId, ProjectUserProjection projection)
        {
            _cache.TryAdd(projectId, projection);
        }
        public bool Remove(Guid projectId)
        {
            return _cache.TryRemove(projectId, out _);
        }
    }
}
