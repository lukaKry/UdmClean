using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UdmClean.Application.DTOs.LeaveAllocation;
using UdmClean.Application.Responses;

namespace UdmClean.Application.Features.LeaveAllocations.Requests.Commands
{
    public class UpdateLeaveAllocationCommand : IRequest<BaseCommandResponse>
    {
        public UpdateLeaveAllocationDto LeaveAllocationDto { get; set; }
    }
}
