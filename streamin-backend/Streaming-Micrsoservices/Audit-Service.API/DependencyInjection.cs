using Audit_Service.API.Infrastructure.BufferService;
using Audit_Service.API.Infrastructure.Messaging.Connection;
using System.Runtime.CompilerServices;

namespace Audit_Service.API
{
    public static class DependencyInjection
    {
        public static void AddDependency(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionManager,ConnectionManager>();
            services.AddHostedService<BufferWriterJob>();
        }
    }
}
