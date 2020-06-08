using DAL.Data;
using DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Services
{

    public interface IOperationService
    {
        public Task<List<Operation>> GetOperationsAsync();
        public Task<Operation> GetOperationByIdAsync(string id);
        public Task<Operation> InsertOperationAsync(Operation operation);
        public Task<Operation> UpdateOperationAsync(string id, Operation o);
        public Task<Operation> DeleteOperationAsync(string id);
        public List<Operation> GetOperationsBySphereId(string id);
       
    }
    public class OperationService : IOperationService
    {
        ApplicationDbContext _context;
        public OperationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Operation> DeleteOperationAsync(string id)
        {
            var Operation = await _context.Operations.FindAsync(id);

            if (Operation == null)
                return null;

            _context.Operations.Remove(Operation);
            await _context.SaveChangesAsync();

            return Operation;
        }

        public async Task<Operation> GetOperationByIdAsync(string id)
        {
            return await _context.Operations.FindAsync(id);
        }

        public async Task<List<Operation>> GetOperationsAsync()
        {
            return await _context.Operations.ToListAsync();
        }

        public List<Operation> GetOperationsBySphereId(string sphereId)
        {
            return _context.Operations.Where(s => s.SphereId == sphereId).ToList();
        }


        public async Task<Operation> InsertOperationAsync(Operation operation)
        {
            _context.Operations.Add(operation);
            await _context.SaveChangesAsync();

            return operation;
        }

        public async Task<Operation> UpdateOperationAsync(string id, Operation o)
        {
            var Operation = await _context.Operations.FindAsync(id);

            if (Operation == null)
                return null;

            Operation.Name = o.Name;

            _context.Operations.Update(Operation);
            await _context.SaveChangesAsync();

            return Operation;
        }

    }
}