using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace UdmClean.Application.Features.LeaveAllocations.Requests.Commads
{
    public class DeleteLeaveAllocationCommand : IRequest
    {
        public int Id { get; set; }
    }
}
