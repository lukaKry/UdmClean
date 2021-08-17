using AutoMapper;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UdmClean.Application.Contracts.Persistance;
using UdmClean.Application.DTOs.LeaveAllocation;
using UdmClean.Application.DTOs.LeaveType;
using UdmClean.Application.Exceptions;
using UdmClean.Application.Features.LeaveAllocations.Handlers.Commands;
using UdmClean.Application.Features.LeaveAllocations.Requests.Commands;
using UdmClean.Application.Features.LeaveTypes.Handlers.Commands;
using UdmClean.Application.Features.LeaveTypes.Requests.Commands;
using UdmClean.Application.Profiles;
using UdmClean.Application.Responses;
using UdmClean.Application.UnitTests.Mocks;
using Xunit;

namespace UdmClean.Application.UnitTests.LeaveAllocations.Commands
{
    public class UpdateLeaveRequestCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _mockLeaveAllocationRepo;
        private readonly UpdateLeaveAllocationCommandHandler _handler;
        private UpdateLeaveAllocationDto _updateLeaveAllocationDto;

        public UpdateLeaveRequestCommandHandlerTest()
        {
            var mapperConfig = new MapperConfiguration( c => c.AddProfile<MappingProfile>());
            _mapper = mapperConfig.CreateMapper();

            _mockLeaveAllocationRepo = MockLeaveAllocationRepository.GetLeaveAllocationRepository().Object;

            _updateLeaveAllocationDto = new UpdateLeaveAllocationDto()
            {
                Id = 1,
                NumberOfDays = 1,
                LeaveTypeId = 2,
                Period = DateTime.Now.Year
            };

            var mockLeaveTypeRepo = MockLeaveTypeRepository.GetLeaveTypeRepository().Object;

            _handler = new UpdateLeaveAllocationCommandHandler(_mockLeaveAllocationRepo, _mapper, mockLeaveTypeRepo);
        }

        [Fact]
        public async Task Handle_ValidCommandData_UpdatesRecordInRepository()
        {
            await _handler.Handle(new UpdateLeaveAllocationCommand() { LeaveAllocationDto = _updateLeaveAllocationDto }, CancellationToken.None);

            var result = await _mockLeaveAllocationRepo.GetAsync(1);

            result.NumberOfDays.ShouldBe(1);
            result.LeaveTypeId.ShouldBe(2);
            result.Period.ShouldBe(DateTime.Now.Year);
        }

        [Fact]
        public async Task Handle_IncorrectPeriodValue_ThrowsValidationException()
        {
            _updateLeaveAllocationDto.Period = 0;

            var response = await _handler.Handle(new UpdateLeaveAllocationCommand() { LeaveAllocationDto = _updateLeaveAllocationDto }, CancellationToken.None);

            response.ShouldBeOfType<BaseCommandResponse>();
            response.Success.ShouldBeFalse();

            _updateLeaveAllocationDto.Period = DateTime.Now.Year;
        }

        [Fact]
        public async Task Handle_LeaveTypeIdLowerThanZero_ThrowsValidationException()
        {
            _updateLeaveAllocationDto.LeaveTypeId = -1;

            var response = await _handler.Handle(new UpdateLeaveAllocationCommand() { LeaveAllocationDto = _updateLeaveAllocationDto }, CancellationToken.None);

            response.ShouldBeOfType<BaseCommandResponse>();
            response.Success.ShouldBeFalse();
        }

        [Fact]
        public async Task Handle_NonExistingLeaveTypeId_ThrowsValidationException()
        {
            _updateLeaveAllocationDto.LeaveTypeId = 99;

            var response = await _handler.Handle(new UpdateLeaveAllocationCommand() { LeaveAllocationDto = _updateLeaveAllocationDto }, CancellationToken.None);

            response.ShouldBeOfType<BaseCommandResponse>();
            response.Success.ShouldBeFalse();

            _updateLeaveAllocationDto.LeaveTypeId = 2;
        }

        [Fact]
        public async Task Handle_IncorrectNumberDaysValue_ThrowsValidationException()
        {
            _updateLeaveAllocationDto.NumberOfDays = -1;

            var response = await _handler.Handle(new UpdateLeaveAllocationCommand() { LeaveAllocationDto = _updateLeaveAllocationDto }, CancellationToken.None);

            response.ShouldBeOfType<BaseCommandResponse>();
            response.Success.ShouldBeFalse();
            
            _updateLeaveAllocationDto.NumberOfDays = 1;
        }
    }
}
