using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UdmClean.Application.Exceptions;
using UdmClean.Application.Features.LeaveAllocations.Requests.Commands;
using UdmClean.Application.Contracts.Persistance;
using UdmClean.Domain;
using UdmClean.Application.Responses;

namespace UdmClean.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, BaseCommandResponse>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
        }
        public async Task<BaseCommandResponse> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var leaveAllocation = await _leaveAllocationRepository.GetAsync(request.Id);

            if (leaveAllocation == null)
            {
                response.Success = false;
                response.Message = "Not found error. There is no item with given id.";
            }
            else
            {
                await _leaveAllocationRepository.DeleteAsync(leaveAllocation);

                response.Success = true;
                response.Message = "Item deleted successfully.";
            }
            return response;
        }
    }
}
