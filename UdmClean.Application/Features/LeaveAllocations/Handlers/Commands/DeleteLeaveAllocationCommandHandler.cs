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
using UdmClean.Application.Contracts.Persistence;

namespace UdmClean.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLeaveAllocationCommandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<BaseCommandResponse> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var leaveAllocation = await _unitOfWork.LeaveAllocationRepository.GetAsync(request.Id);

            if (leaveAllocation == null)
            {
                response.Success = false;
                response.Message = "Not found error. There is no item with given id.";
            }
            else
            {
                await _unitOfWork.LeaveAllocationRepository.DeleteAsync(leaveAllocation);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Item deleted successfully.";
            }
            return response;
        }
    }
}
