using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Work_Service.Application.Abstractions;
using Work_Service.Domain.WorkContext;

namespace WorkService.Persistance.UnitOfWork
{
    public sealed class WorkUnitOfWork:IUnitofWork<Workitem>
    {
        private readonly ApplicationbDbContext _context;
        public WorkUnitOfWork(ApplicationbDbContext context)
        {
            _context = context;
        }
        public void Add(Workitem Entity)
        {
            _context.Add(Entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
