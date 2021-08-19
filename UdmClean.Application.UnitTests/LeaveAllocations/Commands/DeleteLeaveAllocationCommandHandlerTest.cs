using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using UdmClean.Application.Contracts.Persistence;
using UdmClean.Application.Features.LeaveAllocations.Handlers.Commands;
using UdmClean.Application.Features.LeaveAllocations.Requests.Commands;
using UdmClean.Application.Responses;
using UdmClean.Application.UnitTests.Mocks;
using Xunit;

namespace UdmClean.Application.UnitTests.LeaveAllocations.Commands
{
    public class DeleteLeaveRequestCommandHandlerTest
    {
        private readonly IUnitOfWork _mockUnitOfWork;
        private readonly DeleteLeaveAllocationCommandHandler _handler;
        public DeleteLeaveRequestCommandHandlerTest()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork().Object;

            _handler = new DeleteLeaveAllocationCommandHandler(_mockUnitOfWork);
        }

        [Fact]
        public async Task Handle_ExistingId_RemoveLeaveAllocationObjectWithGivenId()
        {
            var leaveAllocationCountBefore = _mockUnitOfWork.LeaveAllocationRepository.GetAllAsync().Result.Count;

            await _handler.Handle(new DeleteLeaveAllocationCommand() { Id = 1 }, CancellationToken.None);

            var leaveAllocation = await _mockUnitOfWork.LeaveAllocationRepository.GetAllAsync();

            leaveAllocation.Count.ShouldBe(leaveAllocationCountBefore - 1);

            var searchForDeleteItem = await _mockUnitOfWork.LeaveAllocationRepository.GetAsync(1);
            searchForDeleteItem.ShouldBeNull();
        }

        [Fact]
        public async Task Handle_InvalidId_ThrowsNotFoundException()
        {
            var leaveTypeCountBefore = _mockUnitOfWork.LeaveAllocationRepository.GetAllAsync().Result.Count;

            var response = await _handler.Handle(new DeleteLeaveAllocationCommand() { Id = -1 }, CancellationToken.None);

            var leaveType = await _mockUnitOfWork.LeaveAllocationRepository.GetAllAsync();

            leaveType.Count.ShouldBe(leaveTypeCountBefore);
            response.ShouldBeOfType<BaseCommandResponse>();
            response.Success.ShouldBeFalse();
        }
    }
}
