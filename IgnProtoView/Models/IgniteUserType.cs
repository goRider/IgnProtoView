using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IgnProtoView.Data;

namespace IgnProtoView.Models
{
    public class IgniteUserType
    {
        public int IgniteUserTypeId { get; set; }
        public string IgniteUserTypeName { get; set; }
        public ICollection<IgniteUser> IgniteUsers { get; set; }
    }
}
