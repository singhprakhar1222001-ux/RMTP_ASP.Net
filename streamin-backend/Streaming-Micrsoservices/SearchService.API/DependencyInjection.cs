using SearchService.API.Features.AddIndex;
using SearchService.API.Infrastructure.Buffer;
using SearchService.API.Infrastructure.Cache;
using SearchService.API.Infrastructure.IndexingService;
using SearchService.API.Infrastructure.Messaging.Connection;
using SearchService.API.Infrastructure.Messaging.Topology;


namespace SearchService.API
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services) {
            services.AddSingleton<MessageBuffer>();
            services.AddSingleton<ProjectCache>();
            services.AddSingleton<ProjectUserCache>();
            services.AddSingleton<ElasticClient>();
            services.AddHostedService<IndexingService>();
            services.AddMediatR((x) => x.RegisterServicesFromAssembly(typeof(WorkIndexCommandHandler).Assembly));
            services.AddSingleton<ITopologyInitializer, TopologyInitializer>();
            services.AddSingleton<IConnectionManager, ConnectionManager>();
        }
    }
}
