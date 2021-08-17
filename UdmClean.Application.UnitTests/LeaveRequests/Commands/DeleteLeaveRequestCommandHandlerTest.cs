using AutoMapper;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UdmClean.Application.Contracts.Persistance;
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

namespace UdmClean.Application.UnitTests.LeaveRequests.Commands
{
    public class DeleteLeaveRequestCommandHandlerTest
    {
        private readonly ILeaveAllocationRepository _mockAllocationRepo;
        private readonly DeleteLeaveAllocationCommandHandler _handler;
        public DeleteLeaveRequestCommandHandlerTest()
        {
            _mockAllocationRepo = MockLeaveAllocationRepository.GetLeaveAllocationRepository().Object;
            _handler = new DeleteLeaveAllocationCommandHandler(_mockAllocationRepo);
        }

        [Fact]
        public async Task Handle_ExistingId_RemoveLeaveAllocationObjectWithGivenId()
        {
            var leaveAllocationCountBefore = _mockAllocationRepo.GetAllAsync().Result.Count;

            await _handler.Handle(new DeleteLeaveAllocationCommand() { Id = 1 }, CancellationToken.None);

            var leaveAllocation = await _mockAllocationRepo.GetAllAsync();

            leaveAllocation.Count.ShouldBe(leaveAllocationCountBefore - 1);

            var searchForDeleteItem = await _mockAllocationRepo.GetAsync(1);
            searchForDeleteItem.ShouldBeNull();
        }

        [Fact]
        public async Task Handle_InvalidId_ThrowsNotFoundException()
        {
            var leaveTypeCountBefore = _mockAllocationRepo.GetAllAsync().Result.Count;

            var response = await _handler.Handle(new DeleteLeaveAllocationCommand() { Id = -1 }, CancellationToken.None);
                    
            var leaveType = await _mockAllocationRepo.GetAllAsync();

            leaveType.Count.ShouldBe(leaveTypeCountBefore);

            response.ShouldBeOfType<BaseCommandResponse>();
            response.Success.ShouldBeFalse();
        }
    }
}
