using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Data.Models
{
    public class Appointment
    {
        public string Id { get; set; }
        public string ArtisanId { get; set; }
        public ApplicationUser Artisan { get; set; }
        public string CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public int EstimatedDuration { get; set; }
        public string  Place { get; set; }
        public bool Accepted { get; set; } = false;

        public IList<AppointmentOperations> AppointmentOperations { get; set; }
    }
}
