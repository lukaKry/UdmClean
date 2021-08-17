using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UdmClean.Application.Exceptions;
using UdmClean.Application.Features.LeaveTypes.Requests.Commands;
using UdmClean.Application.Contracts.Persistance;
using UdmClean.Domain;
using UdmClean.Application.Responses;

namespace UdmClean.Application.Features.LeaveTypes.Handlers.Commands
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, BaseCommandResponse>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<BaseCommandResponse> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var leaveType = await _leaveTypeRepository.GetAsync(request.Id);

            if (leaveType == null)
            {
                response.Success = false;
                response.Message = "Not found error. There is no item with given id.";
            }
            else
            {
                await _leaveTypeRepository.DeleteAsync(leaveType);

                response.Success = true;
                response.Message = "Item deleted successfully.";
            }
            return response;
        }
    }
}
