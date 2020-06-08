using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Data.Models
{
    public class ProfileOperation
    {
        public string Id { get; set; }
        public string OperationId { get; set; }
        public Operation Operation { get; set; }
        public string  ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public double Price { get; set; }
        public int Duration { get; set; }
        public string Comments { get; set; }

        public IList<AppointmentOperations> AppointmentOperations { get; set; }
    }
}
