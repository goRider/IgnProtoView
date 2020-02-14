using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IgnProtoView.Models
{
    public class ApplicationStatus
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public ICollection<IgniteUserApplication> IgniteUserApplications { get; set; }
    }
}
