using MediatR;
using System.Runtime.CompilerServices;
using Work_Service.Application.Abstractions;
using Work_Service.Application.Projections;
using Work_Service.Domain.Projections;
using WorkService.Infrastructure.BackgroundJobs;
using WorkService.Infrastructure.Messages.Connection;
using WorkService.Infrastructure.Messages.Topology;
using WorkService.Persistance.UnitOfWork;

namespace Work_Service.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection addDependency(this IServiceCollection service) {
            service.AddSingleton<IConnectionManager, ConnectionManager>();
            service.AddScoped<IUnitofWork<User>, UnitOfWork>();
            service.AddMediatR(typeof(UserCreateRequestCommand).Assembly);
            service.AddHostedService<ConsumerWorker>(); 
            service.AddSingleton<ITopologyInitializer, TopologyInitializer>();
            return service;
        }
    }
}
