using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Data.Models
{
    public class ProfilesSpheres
    {
        public string SphereId { get; set; }
        public Sphere Sphere { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
