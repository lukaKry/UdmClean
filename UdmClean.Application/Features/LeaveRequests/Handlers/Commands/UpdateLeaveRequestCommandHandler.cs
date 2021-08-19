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
using UdmClean.Application.Contracts.Persistence;

namespace UdmClean.Application.Features.LeaveRequests.Handlers.Commands
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLeaveRequestCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _unitOfWork.LeaveRequestRepository.GetAsync(request.Id);

            if (request.UpdateLeaveRequestDto != null)
            {
                var validator = new UpdateLeaveRequestDtoValidator(_unitOfWork.LeaveTypeRepository, _unitOfWork.LeaveRequestRepository);
                var validationResult = await validator.ValidateAsync(request.UpdateLeaveRequestDto);
                if (validationResult.IsValid == false)
                    throw new ValidationException(validationResult);
                _mapper.Map(request.UpdateLeaveRequestDto, leaveRequest);

                await _unitOfWork.LeaveRequestRepository.UpdateAsync(leaveRequest);
                await _unitOfWork.Save();
            }
            else if (request.ChangeLeaveRequestAprovalDto != null)
            {
                await _unitOfWork.LeaveRequestRepository.ChangeApprovalStatus(leaveRequest, request.ChangeLeaveRequestAprovalDto.Approved);
                if (request.ChangeLeaveRequestAprovalDto.Approved.Value)
                {
                    var allocation = await _unitOfWork.LeaveAllocationRepository.GetUserAllocations(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
                    int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;

                    allocation.NumberOfDays -= daysRequested;

                    await _unitOfWork.LeaveAllocationRepository.UpdateAsync(allocation);
                }
                await _unitOfWork.Save();
            }

            return Unit.Value;
        }
    }
}
