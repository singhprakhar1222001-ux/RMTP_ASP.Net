using SearchService.API.Infrastructure.Buffer;
using SearchService.API.Infrastructure.Cache;
using SearchService.API.Infrastructure.IndexingService;


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
        }
    }
}
