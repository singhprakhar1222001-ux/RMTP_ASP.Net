
using Identity.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Identity.API.Helpers;
using Identity.SharedKernel;
using Identity.Infrastructure.Interceptor;
using Identity.Infrastructure.Messages.Topology;

namespace Identity.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<AppIdentityDbContext>((sp,options) =>
        {
            var interceptor = sp.GetServices<EventInterceptor>();
            options.UseNpgsql(builder.Configuration.GetConnectionString("identitydb"))
                 .AddInterceptors(interceptor);
        }
        );

        Console.WriteLine(builder.Configuration.GetConnectionString("identitydb"));
        builder.Services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<AppIdentityDbContext>();


        //extension method for jwt
        builder.Services.AddJwt();

        var app = builder.Build();

        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            using var scope = app.Services.CreateScope();
            var dbcontext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
            var topologyInitializor = scope.ServiceProvider.GetRequiredService<IToplogyInitializor>();
            dbcontext.Database.Migrate();

            
            //initialize exchange

            await topologyInitializor.Initialize();

            
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
