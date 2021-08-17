using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UdmClean.Application.DTOs.LeaveRequest;
using UdmClean.Application.Responses;

namespace UdmClean.Application.Features.LeaveRequests.Requests.Commands
{
    public class UpdateLeaveRequestCommand : IRequest<BaseCommandResponse>
    {
        public UpdateLeaveRequestDto UpdateLeaveRequestDto { get; set; }
        public ChangeLeaveRequestAprovalDto ChangeLeaveRequestAprovalDto { get; set; }
    }
}
