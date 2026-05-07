using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work_Service.Domain.WorkContext
{
    public class ProjectBase
    {
        private ProjectBase(Guid guid, string Name, string Description, List<Guid> userDetails, Guid ProjectHead)
        {
            Id = guid;
            this.Name = Name;
            this.Description = Description;
            this.ProjectHead = ProjectHead;
            this.ProjectMembers = userDetails;

        }
        public Guid Id { get;}
        public string Name { get; private set; }

        public string Description { get; private set; }

        public Guid ProjectHead { get;private set; }

        public IReadOnlyCollection<Guid> ProjectMembers { get; private set; }

        public static ProjectBase? CreateProject(List <Guid> members, string Name, string Description, Guid Projecthead)
        {
            if(Name.Length<7 || Name[0] >90 || Name[0]<65)
            {
                throw new UserCreationException("User validation failed");
            }
            Guid guid = Guid.NewGuid();
            return new ProjectBase(guid, Name, Description, members, Projecthead);
            
        }
    }


    public enum Role
    {
        Manager, 
        SubManager,
        Employee
    }
    public record UserDetails(string Name, Role Role);

    public class UserCreationException : Exception
    {
        public UserCreationException(string message) : base(message)
        {

        }
    }
}
