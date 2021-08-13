using System;
using System.Collections.Generic;
using System.Text;
using UdmClean.Application.DTOs.Common;

namespace UdmClean.Application.DTOs.LeaveRequest
{
    public class ChangeLeaveRequestAprovalDto : BaseDto
    {
        public bool? Approved { get; set; }
    }
}
