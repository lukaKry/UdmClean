using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdmClean.Application.Contracts.Persistance;
using UdmClean.Domain;

namespace UdmClean.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly UdmCleanDbContext _dbContext;

        public LeaveAllocationRepository(UdmCleanDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAllocations(List<LeaveAllocation> allocations)
        {
            await _dbContext.AddRangeAsync(allocations);
        }

        public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
        {
            return await _dbContext.LeaveAllocations.AnyAsync(q => q.EmployeeId == userId && q.LeaveTypeId == leaveTypeId && q.Period == period);
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        {
            return await _dbContext.LeaveAllocations.Include(p => p.LeaveType).ToListAsync();
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync(string userId)
        {
            var leaveAllocations = await _dbContext.LeaveAllocations.Where(q => q.EmployeeId == userId)
                .Include(q => q.LeaveType)
                .ToListAsync();
            return leaveAllocations;
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            return await _dbContext.LeaveAllocations.Include(p => p.LeaveType).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
        {
            return await _dbContext.LeaveAllocations.FirstOrDefaultAsync(q => q.EmployeeId == userId && q.LeaveTypeId == leaveTypeId);
        }
    }
}
