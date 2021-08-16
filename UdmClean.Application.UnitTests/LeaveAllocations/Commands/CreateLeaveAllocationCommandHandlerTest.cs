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
using UdmClean.Application.UnitTests.Mocks;
using Xunit;

namespace UdmClean.Application.UnitTests.LeaveAllocations.Commands
{
    public class CreateLeaveRequestCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _mockAllocationRepo;
        private readonly CreateLeaveAllocationDto _createLeaveAllocationDto;
        private readonly CreateLeaveAllocationCommandHandler _handler;
        public CreateLeaveRequestCommandHandlerTest()
        {
            _mockAllocationRepo = MockLeaveAllocationRepository.GetLeaveAllocationRepository().Object;

            var mapperConfig = new MapperConfiguration(p => p.AddProfile<MappingProfile>());
            _mapper = mapperConfig.CreateMapper();
            _createLeaveAllocationDto = new CreateLeaveAllocationDto()
            {
                NumberOfDays = 1,
                LeaveTypeId = 1,
                Period = DateTime.Now.Year
            };

            var mockTypeRepository = MockLeaveTypeRepository.GetLeaveTypeRepository().Object;

            _handler = new CreateLeaveAllocationCommandHandler(_mockAllocationRepo, _mapper, mockTypeRepository);

        }

        [Fact]
        public async Task Handle_ValidRequestData_ReturnsIdOfNewObject()
        {
            var leaveAllocationsCountBefore = _mockAllocationRepo.GetAllAsync().Result.Count;

            var result = await _handler.Handle(new CreateLeaveAllocationCommand() { LeaveAllocationDto = _createLeaveAllocationDto }, CancellationToken.None);

            var leaveAllocations = await _mockAllocationRepo.GetAllAsync();

            result.ShouldBeOfType<int>();
            leaveAllocations.Count.ShouldBe(leaveAllocationsCountBefore + 1);
        }

        [Fact]
        public async Task Handle_InvalidRequestData_ThrowsValidationException()
        {
            _createLeaveAllocationDto.NumberOfDays = -1;
            
            var leaveAllocationssBefore = await _mockAllocationRepo.GetAllAsync();

            ValidationException ex = await Should.ThrowAsync<ValidationException> ( async () => 
                    await _handler.Handle(new CreateLeaveAllocationCommand() { LeaveAllocationDto = _createLeaveAllocationDto }, CancellationToken.None));

            var leaveAllocationsAfter= await _mockAllocationRepo.GetAllAsync();

            leaveAllocationsAfter.Count.ShouldBe(leaveAllocationssBefore.Count);

            ex.ShouldNotBeNull();
        }
    }
}
