using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Work_Service.Domain.ProjectContext;

namespace Work_Service.Domain.ProjectUser
{
    public enum UserRole
    {
        Member,
        Manager
    }
    public class ProjectUser
    {
        private ProjectUser() { }
        public ProjectUser(Guid id, string name, Role role)
        {
            Id = id;
            Name = name;
            _role = role;
        }
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public Role _role { get; private set; }

        public Guid ProjectId { get; private set; }

        public static ProjectUser CreateProjectUser(Guid id, string Name, Role role)
        {
            ProjectUser user = new ProjectUser(id, Name, role);
            return user;
        }
    }
}
