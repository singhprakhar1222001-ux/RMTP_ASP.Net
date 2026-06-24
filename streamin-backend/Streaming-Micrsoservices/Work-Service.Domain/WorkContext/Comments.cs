using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work_Service.Domain.WorkContext
{
    public class Comments
    {
        public Guid WorkId { get; private set; } 
        public Guid? Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Comment { get;private set; }
        public DateTime Timestamp { get;private set; }

        private Comments() { }
        
        public  Comments(string comment, Guid workId, Guid userId)
        {
            WorkId = workId;
            Id = userId;
            this.Comment = comment;
            Timestamp = DateTime.Now;
            Id=Guid.NewGuid();
        }

    }
}
