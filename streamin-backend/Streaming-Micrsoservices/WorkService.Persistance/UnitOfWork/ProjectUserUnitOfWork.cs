using System;
using System.Collections.Generic;
using System.Text;
using Work_Service.Application.Abstractions;
using Work_Service.Domain.ProjectUser;

namespace WorkService.Persistance.UnitOfWork
{
    public class ProjectUserUnitOfWork:IUnitofWork<ProjectUser>
    {
        private readonly ApplicationbDbContext _context;
        public ProjectUserUnitOfWork(ApplicationbDbContext context)
        {
            _context = context;
        }

        public void Add(ProjectUser Entity)
        {
            _context.Add(Entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
