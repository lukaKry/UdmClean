using System;
using System.Collections.Generic;
using System.Text;
using UdmClean.Application.DTOs.Common;

namespace UdmClean.Application.DTOs.LeaveRequest
{
    public class UpdateLeaveRequestDto : BaseDto, ILeaveRequestDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int LeaveTypeId { get; set; }
        public string RequestComments { get; set; }
        public bool Canceled { get; set; }
    }
}
