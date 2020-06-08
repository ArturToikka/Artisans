using DAL.Data;
using DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Services
{
    public interface IAppUserService
    {
        public Task<List<ApplicationUser>> GetAppUsersAsync();
        public Task<ApplicationUser> GetAppUserByIdAsync(string id);
        public Task<ApplicationUser> InsertAppUserAsync(ApplicationUser ApplicationUser);
        public Task<ApplicationUser> UpdateAppUserAsync(ApplicationUser p);
        public Task<ApplicationUser> DeleteAppUserAsync(string id);
        public Task<ProfilesSpheres> AddSphereToProfile(Sphere sphereId, ApplicationUser profileId);
        public Task<List<ApplicationUser>> GetUsersOfSphere(string sId);
        public Task<List<ApplicationUser>> GetUsersOfOperation(string oId);
        public Task<bool> CheckIfUserHasSphere(string uId, string sId);
        public Task<bool> CheckIfUserHasOperation(string uId, string oId);
        public Task<List<Sphere>> GetAllUserSpheres(string uId);

    }

    public class AppUserService : IAppUserService
    {
        ApplicationDbContext _context;

        public AppUserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProfilesSpheres> AddSphereToProfile(Sphere s, ApplicationUser p)
        {
            var profileSphere = new ProfilesSpheres();
            profileSphere.ApplicationUser = p;
            profileSphere.ApplicationUserId = p.Id;
            profileSphere.Sphere = s;
            profileSphere.SphereId = s.Id;
            _context.ProfilesSpheres.Add(profileSphere);

            await _context.SaveChangesAsync();

            return profileSphere;
        }

        public async Task<List<ApplicationUser>> GetAppUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetAppUserByIdAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<ApplicationUser> InsertAppUserAsync(ApplicationUser applicationUser)
        {
            _context.Users.Add(applicationUser);
            await _context.SaveChangesAsync();

            return applicationUser;
        }

        public async Task<ApplicationUser> UpdateAppUserAsync(ApplicationUser p)
        {
            var User = await _context.Users.FindAsync(p.Id);

            if (User == null)
                return null;

            User.FirstName = p.FirstName;
            User.LastName = p.LastName;
            User.ProfileName = p.ProfileName;
            User.ContactEmail = p.ContactEmail;
            User.Address = p.Address;
            User.ContactPhoneNumber = p.ContactPhoneNumber;

            _context.Users.Update(User);
            await _context.SaveChangesAsync();

            return User;
        }

        public async Task<ApplicationUser> DeleteAppUserAsync(string id)
        {
            var ApplicationUser = await _context.Users.FindAsync(id);

            if (ApplicationUser == null)
                return null;

            _context.Users.Remove(ApplicationUser);
            await _context.SaveChangesAsync();

            return ApplicationUser;
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        public async Task<List<ApplicationUser>> GetUsersOfSphere(string sId)
        {
            var users = _context.ProfilesSpheres
                .Where(p => p.SphereId == sId)
                .ToList();

            List<ApplicationUser> artisans = new List<ApplicationUser>();

            foreach(ProfilesSpheres u in users)
            {
                artisans.Add(
                    await GetAppUserByIdAsync(u.ApplicationUserId)
                    );
            }
            return artisans;
        }

        public async Task<bool> CheckIfUserHasSphere(string uId, string sId)
        {
            bool has = false;

            var x = await (from s in _context.ProfilesSpheres
                           where s.ApplicationUserId == uId && s.SphereId == sId 
                           select s).ToListAsync();

                return has = x.Any(u => u.SphereId == sId);
        }

        public async Task<bool> CheckIfUserHasOperation(string uId, string oId)
        {
            bool has = false;

            var ops = await (from x in _context.ProfileOperations
                             where x.ApplicationUserId == uId && x.OperationId == oId
                             select x).ToListAsync().ConfigureAwait(false);

               return has = ops.Any(u => u.OperationId == oId);
            
        }

        public async Task<List<Sphere>> GetAllUserSpheres(string uId)
        {
            var ps = await _context.ProfilesSpheres
                .Where(s => s.ApplicationUserId  == uId)
                .ToListAsync();

            List<Sphere> spheres = new List<Sphere>();

            foreach (ProfilesSpheres s in ps)
            {
                spheres.Add(
                     await _context.Spheres.FindAsync(s.SphereId)
                    );
            }
            return spheres;
        }

        public async Task<List<ApplicationUser>> GetUsersOfOperation(string oId)
        {
            var users = _context.ProfileOperations
               .Where(p => p.OperationId == oId)
               .ToList();

            List<ApplicationUser> artisans = new List<ApplicationUser>();

            foreach (ProfileOperation u in users)
            {
                artisans.Add(
                    await GetAppUserByIdAsync(u.ApplicationUserId)
                    );
            }
            return artisans;
        }
    }

       
}
