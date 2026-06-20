using Contracts;
using Identity.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Service.Messaging
{
    public class EventContext : IEventContext
    {
        private List<IIntegreationEvent> _events;
        public EventContext()
        {
            _events= new List<IIntegreationEvent>();
        }
        public void AddEvent(IIntegreationEvent integreationEvent)
        {
            _events.Add(integreationEvent);
        }

        public void DeleteEvent()
        {
            _events.Clear();
        }

        public IReadOnlyCollection<IIntegreationEvent> GetEvents()
        {
            return _events;
        }
    }
}
