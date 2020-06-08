using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Data.Models
{
    public class Operation
    {
        public string Id { get; set; }
        public string SphereId { get; set; }
        public  Sphere Sphere { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        public IList<ProfileOperation> ProfileOperation { get; set; }

    }
}
