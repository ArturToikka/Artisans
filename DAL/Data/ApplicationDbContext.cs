using DAL.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Sphere> Spheres { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<ProfilesSpheres> ProfilesSpheres { get; set; }
        public DbSet<ProfileOperation> ProfileOperations { get; set; }
        public DbSet<AppointmentOperations> AppointmentOperations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfilesSpheres>().HasKey(ps => new { ps.ApplicationUserId, ps.SphereId });
            modelBuilder.Entity<ProfileOperation>().HasKey(po => new { po.ApplicationUserId, po.OperationId });
            modelBuilder.Entity<AppointmentOperations>().HasKey(ao => new { ao.AppointmentId, ao.ProfileOperationId });

            base.OnModelCreating(modelBuilder);
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
