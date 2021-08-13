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
    }
}
