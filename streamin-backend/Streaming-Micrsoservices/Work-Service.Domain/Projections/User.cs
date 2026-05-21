using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Work_Service.Domain.ProjectContext;

namespace Work_Service.Domain.Projections
{
    public class User
    {
        private Guid Userid {  get; set; }

        private Role role { get; set; }

        private string name { get; set; }

        private string email { get; set; }

        private User(Guid userid, Role role, string name, string email)
        {
            Userid = userid;
            this.role = role;
            this.name = name;
            this.email = email;
        }
    }
}
