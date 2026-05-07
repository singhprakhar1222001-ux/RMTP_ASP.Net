
using Identity.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Identity.API.Helpers;
using Identity.SharedKernel;

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
        builder.Services.AddDbContext<AppIdentityDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("identitydb"));
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
            dbcontext.Database.Migrate();

            var roleService = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if(!await roleService.RoleExistsAsync(Roles.Head))
            {
                await roleService.CreateAsync(new IdentityRole(Roles.Head));
            }
            if(!await roleService.RoleExistsAsync(Roles.Member))
            {
                await roleService.CreateAsync(new IdentityRole(Roles.Member));
            }
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
