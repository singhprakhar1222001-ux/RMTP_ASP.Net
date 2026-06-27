using Contracts.WorkService.Consts;
using System.Xml.Linq;

namespace SearchService.API.IndexClass
{
    public class WorkIndexBody
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public Guid ProjectId { get; private set; }
        public string description { get; private set; }

        public IReadOnlyList<Comments> _Comment;//has to be updated as comment may be a cascading effect

        public Guid assignedId { get; private set; }
        public string AssignedName { get; private set; }
        

        public Guid managerId { get; private set; }
        public string ManagerName { get; private set; }


        public DateOnly AssignmentDate { get; private set; }

        public DateOnly Deadline { get; private set; }
        public bool IsOverDue { get; private set; }
        public WorkStatus WorkStatus { get; private set; }
        public int Version { get; private set; }
    }

    public class Comments
    {
        public string comment { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime Timestamp {  get; set; }    
    }
}
