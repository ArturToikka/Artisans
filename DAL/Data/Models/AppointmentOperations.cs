using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Data.Models
{
    public class AppointmentOperations
    {
        public string AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
        public string ProfileOperationId { get; set; }
        public ProfileOperation ProfileOperation { get; set; }
    }
}
