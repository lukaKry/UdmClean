using System;
using System.Collections.Generic;
using System.Text;
using UdmClean.Application.Contracts.Persistance;
using UdmClean.Domain;

namespace UdmClean.Persistence.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        private readonly UdmCleanDbContext _dbContext;

        public LeaveTypeRepository(UdmCleanDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
