using AutoMapper;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UdmClean.Application.Contracts.Persistance;
using UdmClean.Application.Contracts.Persistence;
using UdmClean.Application.DTOs.LeaveType;
using UdmClean.Application.Exceptions;
using UdmClean.Application.Features.LeaveTypes.Handlers.Commands;
using UdmClean.Application.Features.LeaveTypes.Requests.Commands;
using UdmClean.Application.Profiles;
using UdmClean.Application.UnitTests.Mocks;
using Xunit;

namespace UdmClean.Application.UnitTests.LeaveTypes.Commands
{
    public class DeleteLeaveAllocationCommandHandlerTest
    {
        private readonly IUnitOfWork _mockUow;
        private readonly DeleteLeaveTypeCommandHandler _handler;
        public DeleteLeaveAllocationCommandHandlerTest()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork().Object;
            _handler = new DeleteLeaveTypeCommandHandler(_mockUow);
        }

        [Fact]
        public async Task Handle_ExistingId_RemoveLeaveTypeObjectWithGivenId()
        {
            var leaveTypeCountBefore = _mockUow.LeaveTypeRepository.GetAllAsync().Result.Count;

            await _handler.Handle(new DeleteLeaveTypeCommand() { Id = 1 }, CancellationToken.None);

            var leaveType = await _mockUow.LeaveTypeRepository.GetAllAsync();

            leaveType.Count.ShouldBe(leaveTypeCountBefore - 1);

            var searchForDeleteItem = await _mockUow.LeaveTypeRepository.GetAsync(1);
            searchForDeleteItem.ShouldBeNull();
        }

        [Fact]
        public async Task Handle_InvalidId_ThrowsNotFoundException()
        {
            var leaveTypeCountBefore = _mockUow.LeaveTypeRepository.GetAllAsync().Result.Count;

            var response = await _handler.Handle(new DeleteLeaveTypeCommand() { Id = -1 }, CancellationToken.None);

            response.Success.ShouldBeFalse();

            var leaveType = await _mockUow.LeaveTypeRepository.GetAllAsync();

            leaveType.Count.ShouldBe(leaveTypeCountBefore);
        }
    }
}
