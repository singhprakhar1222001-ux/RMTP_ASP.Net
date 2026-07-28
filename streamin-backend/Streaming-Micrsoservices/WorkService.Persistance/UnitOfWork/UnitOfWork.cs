using Contracts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Work_Service.Application.Abstractions;
using Work_Service.Domain.ProjectContext;
using Work_Service.Domain.Projections;
using WorkService.Persistance.Outbox;

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
        public async Task<User> GetEntity(Guid id)
        {
            var entity = await _context.Set<User>().FirstOrDefaultAsync(x => x.Userid == id);
            return entity;
        }
    }
}
