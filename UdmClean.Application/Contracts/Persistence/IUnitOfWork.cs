using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UdmClean.Application.Contracts.Persistance;

namespace UdmClean.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        ILeaveAllocationRepository LeaveAllocationRepository { get;}
        ILeaveRequestRepository LeaveRequestRepository { get;}
        ILeaveTypeRepository LeaveTypeRepository { get; }
        Task Save();
    }
}
