using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UdmClean.Application.Contracts.Persistance;
using UdmClean.Application.Contracts.Persistence;

namespace UdmClean.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UdmCleanDbContext _context;

        private ILeaveAllocationRepository _leaveAllocationRepository;
        private ILeaveRequestRepository _leaveRequestRepository;
        private ILeaveTypeRepository _leaveTypeRepository;

        public UnitOfWork(UdmCleanDbContext context)
        {
            _context = context;
        }



        public ILeaveAllocationRepository LeaveAllocationRepository => _leaveAllocationRepository ??= new LeaveAllocationRepository(_context);

        public ILeaveRequestRepository LeaveRequestRepository => _leaveRequestRepository ??= new LeaveRequestRepository(_context);

        public ILeaveTypeRepository LeaveTypeRepository => _leaveTypeRepository ??= new LeaveTypeRepository(_context);
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
