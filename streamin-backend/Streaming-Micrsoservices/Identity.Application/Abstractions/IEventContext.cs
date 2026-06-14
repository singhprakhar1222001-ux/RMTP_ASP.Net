using Contracts.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Abstractions
{
    public interface IEventContext
    {
        public void AddEvent(IIntegreationEvent integreationEvent);
        public IReadOnlyCollection<IIntegreationEvent> GetEvents();
        public void DeleteEvent();
    }
}
