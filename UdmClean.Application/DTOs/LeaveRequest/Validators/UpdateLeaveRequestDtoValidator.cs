using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using UdmClean.Application.Contracts.Persistance;

namespace UdmClean.Application.DTOs.LeaveRequest.Validators
{
    public class UpdateLeaveRequestDtoValidator : AbstractValidator<UpdateLeaveRequestDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public UpdateLeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository leaveRequestRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
            Include(new ILeaveRequestDtoValidator(_leaveTypeRepository));

            RuleFor(p => p.Id)
               .NotNull().WithMessage("{PropertyName} must be present.")
               .GreaterThan(0).WithMessage("{PropertyName} should be greater than {ComparisonValue}.")
               .MustAsync(async (id, token) =>
               {
                   var doesExist = await _leaveRequestRepository.ExistsAsync(id);
                   return !doesExist;
               })
               .WithMessage("{PropertyName} does not exist.");
        }
    }
}
