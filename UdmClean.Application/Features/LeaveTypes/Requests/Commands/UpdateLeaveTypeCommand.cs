using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UdmClean.Application.DTOs.LeaveType;
using UdmClean.Application.Responses;

namespace UdmClean.Application.Features.LeaveTypes.Requests.Commands
{
    public class UpdateLeaveTypeCommand : IRequest<BaseCommandResponse>
    {
        public LeaveTypeDto LeaveTypeDto { get; set; }
    }
}
