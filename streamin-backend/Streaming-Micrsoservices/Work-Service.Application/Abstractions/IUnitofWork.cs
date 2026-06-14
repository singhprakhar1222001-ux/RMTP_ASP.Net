using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work_Service.Application.Abstractions
{
    public interface IUnitofWork<T> where T : class
    {
        public void Add(T Entity);
        public Task SaveChangesAsync();
    }
}
