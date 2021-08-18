using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdmClean.UI.Services.Base;

namespace UdmClean.UI.Contracts
{
    public interface ILeaveAllocationService
    {
        Task<Response<int>> CreateLeaveAllocations(int leaveTypeId);
    }
}
