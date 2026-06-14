using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Work_Service.Application.Abstractions;
using Work_Service.Domain.Projections;

namespace WorkService.Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitofWork<User>
    {
        private readonly ApplicationbDbContext _context;
        public UnitOfWork(ApplicationbDbContext context)
        {
            _context= context;
        }
        public void Add(User Entity)
        {
            _context.Add(Entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
