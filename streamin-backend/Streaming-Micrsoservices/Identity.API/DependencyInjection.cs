using Identity.Application.Abstractions;
using Identity.Application.Service.Jwt;
using Identity.Application.Service.Messaging;
using Identity.Infrastructure.BackgroundJobs;
using Identity.Infrastructure.Interceptor;
using Identity.Infrastructure.Jwt;
using Identity.Infrastructure.Messages.Connection;
using Identity.Infrastructure.Messages.Topology;
using RabbitMQ.Client;
using System.Runtime.CompilerServices;

namespace Identity.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddJwt(this IServiceCollection services)
        {
            services.AddScoped<IJwtService,JwtService>();
            services.AddScoped<IEventContext, EventContext>();//scoped to maintain outbox sanity and conssitency as it is tied to transaction, extremely important to not forget it.
            services.AddSingleton<EventInterceptor>();
            services.AddSingleton<IConnectionCreator, Connection>();
            services.AddHostedService<PublishJob>();
            services.AddSingleton<IToplogyInitializor,TopologyInitializor>();
            return services;
        }
    }
}
