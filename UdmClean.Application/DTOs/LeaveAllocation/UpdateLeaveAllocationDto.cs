using System;
using System.Collections.Generic;
using System.Text;
using UdmClean.Application.DTOs.Common;

namespace UdmClean.Application.DTOs.LeaveAllocation
{
    public class UpdateLeaveAllocationDto : BaseDto, ILeaveAllocationDto
    {
        public int NumberOfDays { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
