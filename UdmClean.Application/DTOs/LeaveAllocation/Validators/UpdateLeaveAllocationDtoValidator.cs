using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using UdmClean.Application.Contracts.Persistance;

namespace UdmClean.Application.DTOs.LeaveAllocation.Validators
{
    public class UpdateLeaveAllocationDtoValidator : AbstractValidator<UpdateLeaveAllocationDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public UpdateLeaveAllocationDtoValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _leaveTypeRepository = leaveTypeRepository;
            Include(new ILeaveAllocationDtoValidator(_leaveTypeRepository));

            RuleFor(p => p.Id)
               .NotNull().WithMessage("{PropertyName} must be present.")
               .GreaterThan(0).WithMessage("Value of {PropertyName} should be greater than {ComparisonValue}.")
               .MustAsync(async (id, token) =>
               {
                   var doesExist = await _leaveAllocationRepository.ExistsAsync(id);
                   return !doesExist;
               })
               .WithMessage("{PropertyName} does not exist.");
        }
    }
}
