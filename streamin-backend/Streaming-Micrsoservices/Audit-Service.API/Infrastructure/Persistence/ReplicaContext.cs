using Microsoft.EntityFrameworkCore;

namespace Audit_Service.API.Infrastructure.Persistence
{
    public class ReplicaContext(DbContextOptions<ReplicaContext> options):DbContext(options)
    {
        
    }
}
