using Identity.Application.Features;
using Identity.Infrastructure.Jwt;
using System.Runtime.CompilerServices;

namespace Identity.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddJwt(this IServiceCollection services)
        {
            services.AddScoped<IJwtService,JwtService>();
            return services;
        }
    }
}
