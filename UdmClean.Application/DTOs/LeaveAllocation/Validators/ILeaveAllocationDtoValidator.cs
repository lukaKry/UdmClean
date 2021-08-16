using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using UdmClean.Application.Contracts.Persistance;

namespace UdmClean.Application.DTOs.LeaveAllocation.Validators
{
    public class ILeaveAllocationDtoValidator : AbstractValidator<ILeaveAllocationDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public ILeaveAllocationDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            RuleFor(p => p.LeaveTypeId)
               .GreaterThan(0).WithMessage("Value of {PropertyName} should be greater than {ComparisonValue}.")
               .MustAsync(async (id, token) =>
               {
                   var doesExist = await _leaveTypeRepository.ExistsAsync(id);
                   return doesExist;
               })
               .WithMessage("{PropertyName} does not exist.");

            RuleFor(p => p.NumberOfDays)
                .GreaterThan(0).WithMessage("{PropertyName} must by greater than {ComparisonValue}")
                .LessThan(100).WithMessage("{PropertyName} must by less than {ComparisonValue}");

            RuleFor(p => p.Period)
                .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("{PropertyName} must be after {ComparisonValue}.");
        }
    }
}
