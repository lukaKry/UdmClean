using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UdmClean.Application.DTOs.LeaveAllocation;
using UdmClean.Application.DTOs.LeaveType;
using UdmClean.Application.Responses;

namespace UdmClean.Application.Features.LeaveTypes.Requests.Commands
{
    public class CreateLeaveTypeCommand : IRequest<BaseCommandResponse>
    {
        public CreateLeaveTypeDto LeaveTypeDto { get; set; }
    }
}
