using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Identity.Consts
{
    public static class UserPermissions
    {
        public const string Create = "user.create";
        public const string Delete = "user.delete";
        public const string Update = "user.update";
    }
    public static class ProjectPermission
    {
        public const string Create = "project.create";
        public const string Update = "project.update";
        public const string Delete = "project.delete";
    }
    public static class WorkPermissions
    {
        public const string Create = "work.create";
        public const string Update = "work.update";
        public const string Delete = "work.delete";
    }
}
