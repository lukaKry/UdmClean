using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using UdmClean.Application.Contracts.Persistance;

namespace UdmClean.Application.DTOs.LeaveAllocation.Validators
{
    public class CreateLeaveAllocationDtoValidator : AbstractValidator<CreateLeaveAllocationDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveAllocationDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            RuleFor(p => p.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
               {
                   var leaveTypeExists = await _leaveTypeRepository.ExistsAsync(id);
                   return leaveTypeExists;
               })
                .WithMessage("{PropertyName} does not exist.");
        }
    }
}
