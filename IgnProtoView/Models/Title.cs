using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IgnProtoView.Data;

namespace IgnProtoView.Models
{
    public class Title
    {
        public int TitleId { get; set; }
        public string TitleName { get; set; }
        public ICollection<IgniteUser> IgniteUsers { get; set; }
    }
}
