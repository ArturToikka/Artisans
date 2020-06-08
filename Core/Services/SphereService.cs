using DAL.Data;
using DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Services
{
    public interface ISphereService
    {
        public Task<List<Sphere>> GetSpheresAsync();
        public Task<Sphere> GetSphereByIdAsync(string id);
        public Task<Sphere> InsertSphereAsync(Sphere sphere);
        public Task<Sphere> UpdateSphereAsync(string id, Sphere s);
        public Task<Sphere> DeleteSphereAsync(string id);
    }
    public class SphereService : ISphereService
    {
        ApplicationDbContext _context;
        public SphereService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Sphere> DeleteSphereAsync(string id)
        {
            var Sphere = await _context.Spheres.FindAsync(id);

            if (Sphere == null)
                return null;

            _context.Spheres.Remove(Sphere);
            await _context.SaveChangesAsync();

            return Sphere;
        }

        public async Task<Sphere> GetSphereByIdAsync(string id)
        {
            return await _context.Spheres.FindAsync(id);
        }

        public async Task<List<Sphere>> GetSpheresAsync()
        {
                return await _context.Spheres.ToListAsync();
        }

        public async Task<Sphere> InsertSphereAsync(Sphere sphere)
        {
            _context.Spheres.Add(sphere);
            await _context.SaveChangesAsync();

            return sphere;
        }

        public async Task<Sphere> UpdateSphereAsync(string id, Sphere s)
        {
            var Sphere = await _context.Spheres.FindAsync(id);

            if (Sphere == null)
                return null;

            Sphere.Name = s.Name;

            _context.Spheres.Update(Sphere);
            await _context.SaveChangesAsync();

            return Sphere;
        }
    }
}
