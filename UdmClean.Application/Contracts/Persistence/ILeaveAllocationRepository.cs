using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UdmClean.Domain;

namespace UdmClean.Application.Contracts.Persistance
{
    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
    {
        Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);
        Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails();

        Task<bool> AllocationExists(string userId, int leaveTypeId, int period);
        Task AddAllocations(List<LeaveAllocation> allocations);
        Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId);
        Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync(string userId);
    }
}
