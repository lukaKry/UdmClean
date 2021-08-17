using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UdmClean.Application.Responses;

namespace UdmClean.Application.Features.LeaveTypes.Requests.Commands
{
    public class DeleteLeaveTypeCommand : IRequest<BaseCommandResponse>
    {
        public int Id { get; set; }
    }
}
