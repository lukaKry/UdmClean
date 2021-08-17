using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UdmClean.Application.Responses;

namespace UdmClean.Application.Features.LeaveRequests.Requests.Commands
{
    public class DeleteLeaveRequestCommand : IRequest<BaseCommandResponse>
    {
        public int Id { get; set; }
    }
}
