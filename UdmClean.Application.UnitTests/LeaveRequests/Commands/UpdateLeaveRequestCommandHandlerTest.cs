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
using UdmClean.Application.DTOs.LeaveRequest;
using UdmClean.Application.Exceptions;
using UdmClean.Application.Features.LeaveRequests.Handlers.Commands;
using UdmClean.Application.Features.LeaveRequests.Requests.Commands;
using UdmClean.Application.Profiles;
using UdmClean.Application.Responses;
using UdmClean.Application.UnitTests.Mocks;
using Xunit;

namespace UdmClean.Application.UnitTests.LeaveRequests.Commands
{
    public class UpdateLeaveRequestCommandHandlerTest
    {
        private readonly IUnitOfWork _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly UpdateLeaveRequestCommandHandler _handler;
        private UpdateLeaveRequestDto _updateLeaveRequestDto;

        public UpdateLeaveRequestCommandHandlerTest()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork().Object;

            var mapperConfig = new MapperConfiguration( c => c.AddProfile<MappingProfile>());
            _mapper = mapperConfig.CreateMapper();

            _handler = new UpdateLeaveRequestCommandHandler(_mockUnitOfWork, _mapper);

            _updateLeaveRequestDto = new UpdateLeaveRequestDto()
            {
                Id = 1,
                StartDate = DateTime.Now - new TimeSpan(2, 0, 0, 0, 0),
                EndDate = DateTime.Now + new TimeSpan(2, 0, 0, 0, 0),
                RequestComments = "update",
                LeaveTypeId = 2,
            };
        }

        [Fact]
        public async Task Handle_ValidCommandData_UpdatesRecordInRepository()
        {
            await _handler.Handle(new UpdateLeaveRequestCommand() { UpdateLeaveRequestDto = _updateLeaveRequestDto }, CancellationToken.None);

            var result = await _mockUnitOfWork.LeaveRequestRepository.GetAsync(1);

            result.RequestComments.ShouldBe("update");
            result.LeaveTypeId.ShouldBe(2);
        }

        [Fact(Skip = "not implemented yet")]
        public async Task Handle_LeaveTypeIdLowerThanZero_ThrowsValidationException()
        {
            _updateLeaveRequestDto.LeaveTypeId = -1;

            var response = await _handler.Handle(new UpdateLeaveRequestCommand() { UpdateLeaveRequestDto = _updateLeaveRequestDto }, CancellationToken.None);

            // wait for global exception handling
        }

        [Fact(Skip = "not implemented yet")]
        public async Task Handle_NonExistingLeaveTypeId_ReturnsUnsuccessfulBaseCommandResponse()
        {
            _updateLeaveRequestDto.LeaveTypeId = 99;

            var response = await _handler.Handle(new UpdateLeaveRequestCommand() { UpdateLeaveRequestDto = _updateLeaveRequestDto }, CancellationToken.None);

            // wait for global exception handling

            _updateLeaveRequestDto.LeaveTypeId = 2;
        }
    }
}
