using Contracts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Work_Service.Application.Abstractions;
using Work_Service.Domain.ProjectContext;
using WorkService.Persistance.Outbox;

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
        public async Task<ProjectBase> GetEntity(Guid id)
        {
            var entity = await _context.Set<ProjectBase>().FirstOrDefaultAsync(x=>x.Id== id);
            return entity;
        }
    }
}
