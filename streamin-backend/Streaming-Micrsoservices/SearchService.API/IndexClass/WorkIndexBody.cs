using Contracts.WorkService.Consts;
using System.Xml.Linq;

namespace SearchService.API.IndexClass
{
    public class WorkIndexBody
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string description { get; set; }

        public IReadOnlyList<Comments> _Comment;//has to be updated as comment may be a cascading effect

        public Guid assignedId { get; set; }
        public string AssignedName { get; set; }
        

        public Guid managerId { get; set; }
        public string ManagerName { get; set; }


        public DateOnly AssignmentDate { get; set; }

        public DateOnly Deadline { get; set; }
        public bool IsOverDue { get; set; }
        public WorkStatus WorkStatus { get; set; }
        public int Version { get; set; }
    }

    public class Comments
    {
        public string comment { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public DateTime Timestamp {  get; set; }    
    }
}
