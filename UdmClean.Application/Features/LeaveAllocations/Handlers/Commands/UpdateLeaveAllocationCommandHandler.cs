using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UdmClean.Application.DTOs.LeaveAllocation.Validators;
using UdmClean.Application.Exceptions;
using UdmClean.Application.Features.LeaveAllocations.Requests.Commands;
using UdmClean.Application.Contracts.Persistance;
using UdmClean.Application.Responses;
using System.Linq;
using UdmClean.Application.Contracts.Persistence;

namespace UdmClean.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLeaveAllocationCommandHandler(
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateLeaveAllocationDtoValidator(_unitOfWork.LeaveTypeRepository, _unitOfWork.LeaveAllocationRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Data validation error. Check errors for details.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var leaveAllocation = await _unitOfWork.LeaveAllocationRepository.GetAsync(request.LeaveAllocationDto.Id);
                _mapper.Map(request.LeaveAllocationDto, leaveAllocation);
                await _unitOfWork.LeaveAllocationRepository.UpdateAsync(leaveAllocation);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update made successfully.";
                response.Id = leaveAllocation.Id;
            }

            return response;
        }
    }
}
