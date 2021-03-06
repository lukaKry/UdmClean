using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UdmClean.Application.DTOs.LeaveRequest;

namespace UdmClean.Application.Features.LeaveRequests.Requests.Queries
{
    public class GetLeaveRequestDetailRequest : IRequest<LeaveRequestDto>
    {
        public int Id { get; set; }
    }
}
