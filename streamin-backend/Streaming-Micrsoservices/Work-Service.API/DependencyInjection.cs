using MediatR;
using System.Runtime.CompilerServices;
using Work_Service.Application.Abstractions;
using Work_Service.Application.Projections;
using Work_Service.Domain.ProjectContext;
using Work_Service.Domain.Projections;
using Work_Service.Domain.ProjectUser;
using Work_Service.Domain.WorkContext;
using WorkService.Infrastructure.BackgroundJobs;
using WorkService.Infrastructure.Messages.Connection;
using WorkService.Infrastructure.Messages.Topology;
using WorkService.Persistance.Interceptor;
using WorkService.Persistance.UnitOfWork;

namespace Work_Service.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection addDependency(this IServiceCollection service) {
            service.AddSingleton<IConnectionManager, ConnectionManager>();
            service.AddScoped<IUnitofWork<User>, UnitOfWork>();
            service.AddScoped<IUnitofWork<Workitem>, WorkUnitOfWork>();
            service.AddScoped<IUnitofWork<ProjectBase>, ProjectUnitOfWork>();
            service.AddScoped<IUnitofWork<ProjectUser>,ProjectUserUnitOfWork>();
            service.AddMediatR((x) => x.RegisterServicesFromAssembly(typeof(UserCreateRequestCommand).Assembly));
            service.AddHostedService<ConsumerWorker>();
            service.AddSingleton<EventInterceptor>();
            service.AddSingleton<ITopologyInitializer, TopologyInitializer>();
            return service;
        }
    }
}
