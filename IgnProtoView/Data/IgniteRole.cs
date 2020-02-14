using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IgnProtoView.Data
{
    public class IgniteRole : IdentityRole<int>
    {
        public IgniteRole()
        {

        }

        public IgniteRole(string rolename, string newrole)
        {

        }

        public IgniteRole(string roleName) : base(roleName)
        {
        }
    }
}
