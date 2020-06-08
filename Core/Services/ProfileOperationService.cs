using DAL.Data;
using DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Services
{
    public interface IProfileOperationService
    {
        public Task<ProfileOperation> GetProfileOperationById(string pId, string oId);
        public Task<ProfileOperation> AddOperationToProfile(string pId, string oId, ProfileOperation po);
        public Task<ProfileOperation> RemoveOperationFromProfile(string oId, string pid);
        public Task<ProfileOperation> UpdateProfileOperation(ProfileOperation po);
        public Task<List<ProfileOperation>> GetProfileOperations(string pId);
        public Task<List<ProfileOperation>> GetProfilesOperationsOfSphere(string pId, string sId);
        public Task<List<ProfileOperation>> GetProfilesOperationsForProfiles(List<ApplicationUser> profiles);
    }
    public class ProfileOperationService : IProfileOperationService
    {
        ApplicationDbContext _context;
        public ProfileOperationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProfileOperation> AddOperationToProfile(string pId, string oId, ProfileOperation po)
        {
            var pop = new ProfileOperation();
            var p = await _context.Users.FindAsync(pId);
            var o = await _context.Operations.FindAsync(oId);
            pop.Id = Guid.NewGuid().ToString();
            pop.ApplicationUser = p;
            pop.ApplicationUserId = p.Id;
            pop.Operation = o;
            pop.OperationId = o.Id;
            pop.Price = po.Price;
            pop.Duration = po.Duration;
            pop.Comments = po.Comments;
            _context.ProfileOperations.Add(pop);
            await _context.SaveChangesAsync();
            return pop;
        }

        public async Task<ProfileOperation> GetProfileOperationById(string pId, string oId)
        {
            var profOp = await _context.ProfileOperations
                .Where(po => po.ApplicationUserId == pId && po.OperationId == oId).FirstOrDefaultAsync();

            if (profOp == null)
            {
                return null;
            }

            return profOp;
        }

        public async Task<List<ProfileOperation>> GetProfileOperations(string pId)
        {
            var pops = await _context.ProfileOperations.Where(po => po.ApplicationUserId == pId).ToListAsync().ConfigureAwait(false);

            return pops;
        }

        public async Task<List<ProfileOperation>> GetProfilesOperationsForProfiles(List<ApplicationUser> profiles)
        {
            List<ProfileOperation> profOps = new List<ProfileOperation>();

            foreach (ApplicationUser p in profiles)
            {
                var profOp = await _context.ProfileOperations.Where(u => u.ApplicationUserId == p.Id).ToListAsync();
                foreach (ProfileOperation po in profOp)
                {
                    profOps.Add(po);
                }

            }

            return profOps;
        }

        public async Task<List<ProfileOperation>> GetProfilesOperationsOfSphere(string pId, string sId)
        {
            var pops = await _context.ProfileOperations.Where(po => po.ApplicationUserId == pId
            && po.Operation.SphereId == sId).ToListAsync();

            return pops;
        }

        public async Task<ProfileOperation> RemoveOperationFromProfile(string oId, string pId)
        {
            var profOp = await _context.ProfileOperations
                .Where(po => po.ApplicationUserId == pId && po.OperationId == oId).FirstOrDefaultAsync();

            _context.Remove(profOp);
            await _context.SaveChangesAsync();
            return profOp;
        }

        public async Task<ProfileOperation> UpdateProfileOperation(ProfileOperation po)
        {
            var pop = await GetProfileOperationById(po.ApplicationUserId, po.OperationId);

            if (pop == null)
            {
                return null;
            }

            pop.Price = po.Price;
            pop.Duration = po.Duration;
            pop.Comments = po.Comments;

            _context.ProfileOperations.Update(pop);
            await _context.SaveChangesAsync();

            return pop;
        }
    }
}
