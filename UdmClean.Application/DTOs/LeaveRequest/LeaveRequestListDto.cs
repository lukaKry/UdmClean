using System;
using System.Collections.Generic;
using System.Text;
using UdmClean.Application.DTOs.Common;
using UdmClean.Application.DTOs.LeaveType;

namespace UdmClean.Application.DTOs.LeaveRequest
{
    public class LeaveRequestListDto : BaseDto
    {
        public LeaveTypeDto LeaveType { get; set; }
        public DateTime DateRequested { get; set; }
        public bool? Approved { get; set; }
    }
}
