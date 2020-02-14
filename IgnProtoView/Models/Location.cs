using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IgnProtoView.Data;

namespace IgnProtoView.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public string CityLocation { get; set; }
        public string StateLocation { get; set; }
        public string CountryLocation { get; set; }
        public ICollection<IgniteUser> IgniteUsers { get; set; }
    }
}
