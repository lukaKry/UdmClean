using System;
using System.Collections.Generic;
using System.Text;
using UdmClean.Application.DTOs.Common;

namespace UdmClean.Application.DTOs.LeaveType
{
    public class LeaveTypeDto : BaseDto, ILeaveTypeDto
    {
        public string Name { get; set; }
        public int DefaultDays { get; set; }
    }
}
