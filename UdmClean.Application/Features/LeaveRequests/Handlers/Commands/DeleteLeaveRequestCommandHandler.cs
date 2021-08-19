using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UdmClean.Application.Exceptions;
using UdmClean.Application.Features.LeaveRequests.Requests.Commands;
using UdmClean.Application.Contracts.Persistance;
using UdmClean.Domain;
using UdmClean.Application.Responses;
using UdmClean.Application.Contracts.Persistence;

namespace UdmClean.Application.Features.LeaveRequests.Handlers.Commands
{
    public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLeaveRequestCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseCommandResponse> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var leaveRequest = await _unitOfWork.LeaveRequestRepository.GetAsync(request.Id);

            if (leaveRequest == null)
            {
                response.Success = false;
                response.Message = "Data validation error. Check Errors for details.";
            }
            else
            {
                await _unitOfWork.LeaveRequestRepository.DeleteAsync(leaveRequest);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Item deleted successfully.";
            }
            return response;
        }
    }
}
