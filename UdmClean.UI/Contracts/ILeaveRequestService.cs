using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdmClean.UI.Models;
using UdmClean.UI.Services.Base;

namespace UdmClean.UI.Contracts
{
    public interface ILeaveRequestService
    {
        Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestList();
        Task<EmployeeLeaveRequestViewVM> GetUserLeaveRequests();
        Task<Response<int>> CreateLeaveRequest(CreateLeaveRequestVM leaveRequest);
        Task<LeaveRequestVM> GetLeaveRequest(int id);
        Task DeleteLeaveRequest(int id);
        Task ApproveLeaveRequest(int id, bool approved);
    }
}
