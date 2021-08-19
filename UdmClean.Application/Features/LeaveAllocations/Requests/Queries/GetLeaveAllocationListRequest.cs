using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UdmClean.Application.DTOs;
using UdmClean.Application.DTOs.LeaveAllocation;

namespace UdmClean.Application.Features.LeaveAllocations.Requests.Queries
{
    public class GetLeaveAllocationListRequest : IRequest<List<LeaveAllocationDto>>
    {
        public bool IsLoggedInUser { get; set; }
    }
}
