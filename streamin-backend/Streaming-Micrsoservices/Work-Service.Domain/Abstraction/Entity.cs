using Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work_Service.Domain.Abstraction
{
    public class Entity
    {
        private IList<DomainEvents> _events { get; set; } = new List<DomainEvents>();

        public void AddEvent(DomainEvents _event){
            _events.Add(_event);
        }
        public void ClearContext()
        {
            _events.Clear();
        }
        public IEnumerable<DomainEvents> getEvents()
        {
            return _events.ToList();
        }
            
    }
}
