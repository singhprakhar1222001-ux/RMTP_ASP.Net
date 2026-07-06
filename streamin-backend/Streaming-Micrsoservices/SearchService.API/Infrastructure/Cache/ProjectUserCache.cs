using SearchService.API.Infrastructure.Projections.Models;
using System.Collections.Concurrent;

namespace SearchService.API.Infrastructure.Cache
{
    public class ProjectUserCache
    {
        private readonly ConcurrentDictionary<Guid, ProjectUserProjection> _cache = new();

        public bool GetValue(Guid projectUserId, out ProjectUserProjection? projection)
        {

            bool res = _cache.TryGetValue(projectUserId, out projection);
            return res;
        }

        public void Insert(Guid projectUserId, ProjectUserProjection projection)
        {
            _cache.TryAdd(projectUserId, projection);
        }
        public bool Remove(Guid projectUserId)
        {
            return _cache.TryRemove(projectUserId, out _);
        }
    }
}
