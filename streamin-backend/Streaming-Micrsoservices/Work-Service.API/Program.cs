using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RabbitMQ.Client;
using Work_Service.API;
using WorkService.Infrastructure.Messages.Connection;
using WorkService.Infrastructure.Messages.Topology;
using WorkService.Persistance;
using WorkService.Persistance.Interceptor;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




builder.Services.AddAuthentication()
    .AddJwtBearer("jwt",
    o =>
    {
        o.Authority = "https://localhost:7056";
        o.Audience = "workservice";
        o.RequireHttpsMetadata = false;

        o.TokenValidationParameters = new TokenValidationParameters
        {
            
        };
    }
    );

builder.Services.AddAuthorization();
builder.Services.AddDbContext<ApplicationbDbContext>(
    (sp,options) =>
    {
        var interceptor = sp.GetRequiredService<EventInterceptor>();
        options.UseNpgsql(builder.Configuration.GetConnectionString("workservicedb"))
        .AddInterceptors(interceptor);
        ;
    }
    );
builder.Services.addDependency();
var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using var scope = app.Services.CreateScope();
    using var dbcontext= scope.ServiceProvider.GetRequiredService<ApplicationbDbContext>();
    var topologyInitializer = scope.ServiceProvider.GetRequiredService<ITopologyInitializer>();
    await topologyInitializer.Initialize();
    dbcontext.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
