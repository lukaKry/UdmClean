using System;
using System.Collections.Generic;
using System.Text;
using UdmClean.Application.DTOs.Common;
using UdmClean.Application.DTOs.LeaveType;
using UdmClean.Application.Modules.Identity;

namespace UdmClean.Application.DTOs.LeaveRequest
{
    public class LeaveRequestListDto : BaseDto
    {
        public LeaveTypeDto LeaveType { get; set; }
        public DateTime DateRequested { get; set; }
        public bool? Approved { get; set; }
        public Employee Employee { get; set; }
        public string RequestingEmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
