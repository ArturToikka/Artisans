using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Data.Models
{
    public class Sphere
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IList<ProfilesSpheres> ProfilesSpheres { get; set; }
    }
}
