using System;
using System.Collections.Generic;
using System.Text;

namespace UdmClean.Application.DTOs.LeaveType
{
    public interface ILeaveTypeDto
    {
        public string Name { get; set; }
        public int DefaultDays { get; set; }
    }
}
