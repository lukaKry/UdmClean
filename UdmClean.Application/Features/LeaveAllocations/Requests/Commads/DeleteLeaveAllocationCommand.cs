using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UdmClean.Application.Responses;

namespace UdmClean.Application.Features.LeaveAllocations.Requests.Commands
{
    public class DeleteLeaveAllocationCommand : IRequest<BaseCommandResponse>
    {
        public int Id { get; set; }
    }
}
