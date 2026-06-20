using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Work_Service.Domain.WorkContext;

namespace Work_Service.Application.Abstractions.Repos
{
    public interface IWorkRepo
    {
        public void AddWork(Workitem workitem);
    }
}
