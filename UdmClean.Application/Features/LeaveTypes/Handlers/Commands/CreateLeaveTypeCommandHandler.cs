using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UdmClean.Application.DTOs.LeaveType.Validators;
using UdmClean.Application.Exceptions;
using UdmClean.Application.Features.LeaveTypes.Requests.Commands;
using UdmClean.Application.Contracts.Persistance;
using UdmClean.Domain;
using UdmClean.Application.Responses;
using System.Linq;
using UdmClean.Application.Contracts.Persistence;

namespace UdmClean.Application.Features.LeaveTypes.Handlers.Commands
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateLeaveTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.LeaveTypeDto);

            var response = new BaseCommandResponse();
           
            if(validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Data validation error. Check Errors for details.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var leaveType = _mapper.Map<LeaveType>(request.LeaveTypeDto);
                leaveType = await _unitOfWork.LeaveTypeRepository.AddAsync(leaveType);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Creation ended successfully.";
                response.Id = leaveType.Id;
            }
            return response;
        }
    }
}
