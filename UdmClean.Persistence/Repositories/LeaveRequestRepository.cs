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
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly UdmCleanDbContext _dbContext;

        public LeaveRequestRepository(UdmCleanDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? approved)
        {
            leaveRequest.Approved = approved;
            _dbContext.Entry(leaveRequest).State = EntityState.Modified;
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetailsAsync()
        {
            return await _dbContext.LeaveRequests.Include(q => q.LeaveType).ToListAsync();
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetailsAsync(string userId)
        {
            var leaveRequests = await _dbContext.LeaveRequests.Where(q => q.RequestingEmployeeId == userId)
                .Include(q => q.LeaveType)
                .ToListAsync();
            return leaveRequests;
        }

        public async Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(int id)
        {
            // Include() nie działa z Find(); dlatego trzeba uzyc FristOrDefault lub Single
            return await _dbContext.LeaveRequests.Include(p => p.LeaveType).FirstOrDefaultAsync(p=> p.Id == id); 
        }
    }
}
