using System;
using System.Collections.Generic;
using System.Text;
using Work_Service.Application.Abstractions;
using Work_Service.Domain.ProjectContext;

namespace WorkService.Persistance.UnitOfWork
{
    public class ProjectUnitOfWork : IUnitofWork<ProjectBase>
    {
        private readonly ApplicationbDbContext _context;
        public ProjectUnitOfWork(ApplicationbDbContext context)
        {
            _context = context;
        }
        public void Add(ProjectBase Entity)
        {
            _context.Add(Entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
