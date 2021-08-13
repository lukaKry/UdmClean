﻿using System;
using System.Collections.Generic;
using System.Text;
using UdmClean.Application.DTOs.Common;
using UdmClean.Application.DTOs.LeaveType;

namespace UdmClean.Application.DTOs.LeaveAllocation
{
    public class LeaveAllocationDto : BaseDto, ILeaveAllocationDto
    {
        public int NumberOfDays { get; set; }
        public LeaveTypeDto LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
