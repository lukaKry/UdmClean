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
using UdmClean.Application.Features.LeaveTypes.Handlers.Commands;
using UdmClean.Application.Features.LeaveTypes.Requests.Commands;
using UdmClean.Application.Profiles;
using UdmClean.Application.UnitTests.Mocks;
using Xunit;

namespace UdmClean.Application.UnitTests.LeaveTypes.Commads
{
    public class DeleteLeaveTypeCommandHandlerTest
    {
        private readonly ILeaveTypeRepository _mockRepo;
        private readonly DeleteLeaveTypeCommandHandler _handler;
        public DeleteLeaveTypeCommandHandlerTest()
        {
            _mockRepo = MockLeaveTypeRepository.GetLeaveTypeRepository().Object;
            _handler = new DeleteLeaveTypeCommandHandler(_mockRepo);
        }

        [Fact]
        public async Task Handle_ExistingId_RemoveLeaveTypeObjectWithGivenId()
        {
            var leaveTypeCountBefore = _mockRepo.GetAllAsync().Result.Count;

            await _handler.Handle(new DeleteLeaveTypeCommand() { Id = 1 }, CancellationToken.None);

            var leaveType = await _mockRepo.GetAllAsync();

            leaveType.Count.ShouldBe(leaveTypeCountBefore - 1);

            var searchForDeleteItem = await _mockRepo.GetAsync(1);
            searchForDeleteItem.ShouldBeNull();
        }

        [Fact]
        public async Task Handle_InvalidId_ThrowsNotFoundException()
        {
            var leaveTypeCountBefore = _mockRepo.GetAllAsync().Result.Count;

            NotFoundException ex = await Should.ThrowAsync<NotFoundException>(
                async () => 
                    await _handler.Handle(new DeleteLeaveTypeCommand() { Id = -1 }, CancellationToken.None));

            ex.ShouldNotBeNull();

            var leaveType = await _mockRepo.GetAllAsync();

            leaveType.Count.ShouldBe(leaveTypeCountBefore);
        }
    }
}
