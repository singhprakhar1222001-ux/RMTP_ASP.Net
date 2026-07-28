using Contracts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Work_Service.Application.Abstractions;
using Work_Service.Domain.ProjectContext;
using Work_Service.Domain.ProjectUser;
using WorkService.Persistance.Outbox;

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
        public void AddOutbox(IIntegreationEvent integreationEvent)
        {
            var outboxMessage = new OutboxMessage
            {
                Id = Guid.NewGuid(),
                Type = integreationEvent.GetType().ToString(),
                Message = JsonConvert.SerializeObject(integreationEvent),
                IsError = false,
                IsProcessed = false,
                OccuredOn = DateTime.UtcNow
            };
            _context.OutboxMessages.Add(outboxMessage);
        }
        public async Task<ProjectUser> GetEntity(Guid id)
        {
            var entity = await _context.Set<ProjectUser>().FirstOrDefaultAsync(x=>x.Id==id);
            return entity;
        }
    }
}
