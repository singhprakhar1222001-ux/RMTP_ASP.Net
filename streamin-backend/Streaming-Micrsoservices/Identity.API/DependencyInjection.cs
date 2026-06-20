using Google.Protobuf.WellKnownTypes;
using Identity.API.Helpers;
using Identity.Application.Abstractions;
using Identity.Application.Service.Jwt;
using Identity.Application.Service.Messaging;
using Identity.Infrastructure.BackgroundJobs;
using Identity.Infrastructure.Interceptor;
using Identity.Infrastructure.Jwt;
using Identity.Infrastructure.Messages.Connection;
using Identity.Infrastructure.Messages.Topology;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Routing;
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

        public static async Task AddToApp(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var roleService = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleService.RoleExistsAsync(Roles.Head))
            {
                await roleService.CreateAsync(new IdentityRole(Roles.Head));
                
                
            }
            if (!await roleService.RoleExistsAsync(Roles.Member))
            {
                await roleService.CreateAsync(new IdentityRole(Roles.Member));
                


            }

            if (await roleService.RoleExistsAsync(Roles.Admin) == null)
            {

            }
        }

        
        
        
        
        
    }
}
