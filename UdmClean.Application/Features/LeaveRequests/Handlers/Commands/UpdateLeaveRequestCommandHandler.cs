using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UdmClean.Application.DTOs.LeaveRequest.Validators;
using UdmClean.Application.Exceptions;
using UdmClean.Application.Features.LeaveRequests.Requests.Commands;
using UdmClean.Application.Contracts.Persistance;
using UdmClean.Application.Responses;
using System.Linq;

namespace UdmClean.Application.Features.LeaveRequests.Handlers.Commands
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, BaseCommandResponse>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            if (request.UpdateLeaveRequestDto != null)
            {
                var validator = new UpdateLeaveRequestDtoValidator(_leaveTypeRepository, _leaveRequestRepository);
                var validationResult = await validator.ValidateAsync(request.UpdateLeaveRequestDto);
                if (validationResult.IsValid == false)
                {
                    response.Success = false;
                    response.Message = "Data validation error. Check Errors for details.";
                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                }
                else
                {
                    var leaveRequest = await _leaveRequestRepository.GetAsync(request.UpdateLeaveRequestDto.Id);
                    _mapper.Map(request.UpdateLeaveRequestDto, leaveRequest);
                    await _leaveRequestRepository.UpdateAsync(leaveRequest);

                    response.Success = true;
                    response.Message = "Update made successfully.";
                    response.Id = leaveRequest.Id;
                }
            }
            else if (request.ChangeLeaveRequestAprovalDto != null)
            {
                var leaveRequest = await _leaveRequestRepository.GetAsync(request.UpdateLeaveRequestDto.Id);

                if (leaveRequest is null)
                {
                    response.Success = false;
                    response.Message = "Not found error. There is no item with given id.";
                }
                else
                {
                    await _leaveRequestRepository.ChangeApprovalStatus(leaveRequest, request.ChangeLeaveRequestAprovalDto.Approved);
                    
                    response.Success = true;
                    response.Message = "Update made successfully.";
                    response.Id = leaveRequest.Id;
                }
            }
            return response;
        }
    }
}
