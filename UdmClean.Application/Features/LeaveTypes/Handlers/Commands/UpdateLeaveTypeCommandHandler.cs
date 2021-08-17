using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UdmClean.Application.DTOs.LeaveType;
using UdmClean.Application.DTOs.LeaveType.Validators;
using UdmClean.Application.Exceptions;
using UdmClean.Application.Features.LeaveTypes.Requests.Commands;
using UdmClean.Application.Contracts.Persistance;
using UdmClean.Application.Responses;
using System.Linq;

namespace UdmClean.Application.Features.LeaveTypes.Handlers.Commands
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, BaseCommandResponse>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateLeaveTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.LeaveTypeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Data validation error. Check Errors for details.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var leaveType = await _leaveTypeRepository.GetAsync(request.LeaveTypeDto.Id);
                _mapper.Map(request.LeaveTypeDto, leaveType);
                await _leaveTypeRepository.UpdateAsync(leaveType);

                response.Success = true;
                response.Message = "Update made successfully.";
                response.Id = leaveType.Id;
            }
            return response;
        }
    }
}
