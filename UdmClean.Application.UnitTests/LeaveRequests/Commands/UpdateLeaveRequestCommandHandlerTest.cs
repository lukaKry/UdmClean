using AutoMapper;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UdmClean.Application.Contracts.Persistance;
using UdmClean.Application.DTOs.LeaveRequest;
using UdmClean.Application.Exceptions;
using UdmClean.Application.Features.LeaveRequests.Handlers.Commands;
using UdmClean.Application.Features.LeaveRequests.Requests.Commands;
using UdmClean.Application.Profiles;
using UdmClean.Application.UnitTests.Mocks;
using Xunit;

namespace UdmClean.Application.UnitTests.LeaveRequests.Commands
{
    public class UpdateLeaveRequestCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly ILeaveRequestRepository _mockLeaveRequestRepo;
        private readonly UpdateLeaveRequestCommandHandler _handler;
        private UpdateLeaveRequestDto _updateLeaveRequestDto;

        public UpdateLeaveRequestCommandHandlerTest()
        {
            var mapperConfig = new MapperConfiguration( c => c.AddProfile<MappingProfile>());
            _mapper = mapperConfig.CreateMapper();

            _mockLeaveRequestRepo = MockLeaveRequestRepository.GetLeaveRequestRepository().Object;

            _updateLeaveRequestDto = new UpdateLeaveRequestDto()
            {
                Id = 1,
                StartDate = DateTime.Now - new TimeSpan(2, 0, 0, 0, 0),
                EndDate = DateTime.Now + new TimeSpan(2, 0, 0, 0, 0),
                RequestComments = "update",
                LeaveTypeId = 2,
            };

            var mockLeaveTypeRepo = MockLeaveTypeRepository.GetLeaveTypeRepository().Object;

            _handler = new UpdateLeaveRequestCommandHandler(_mockLeaveRequestRepo, mockLeaveTypeRepo, _mapper);
        }

        [Fact]
        public async Task Handle_ValidCommandData_UpdatesRecordInRepository()
        {
            await _handler.Handle(new UpdateLeaveRequestCommand() { UpdateLeaveRequestDto = _updateLeaveRequestDto }, CancellationToken.None);

            var result = await _mockLeaveRequestRepo.GetAsync(1);

            result.RequestComments.ShouldBe("update");
            result.LeaveTypeId.ShouldBe(2);
        }

        [Fact]
        public async Task Handle_LeaveTypeIdLowerThanZero_ThrowsValidationException()
        {
            _updateLeaveRequestDto.LeaveTypeId = -1;

            ValidationException exception = await Should.ThrowAsync<ValidationException>(async () =>
            {
                await _handler.Handle(new UpdateLeaveRequestCommand() { UpdateLeaveRequestDto = _updateLeaveRequestDto }, CancellationToken.None);
            });

            exception.ShouldNotBeNull();
            exception.Errors[0].ShouldContain("be greater than 0");
        }

        [Fact]
        public async Task Handle_NonExistingLeaveTypeId_ThrowsValidationException()
        {
            _updateLeaveRequestDto.LeaveTypeId = 99;

            ValidationException exception = await Should.ThrowAsync<ValidationException>(async () =>
            {
                await _handler.Handle(new UpdateLeaveRequestCommand() { UpdateLeaveRequestDto = _updateLeaveRequestDto }, CancellationToken.None);
            });

            exception.ShouldNotBeNull();
            exception.Errors[0].ShouldContain("does not exist");

            _updateLeaveRequestDto.LeaveTypeId = 2;
        }
    }
}
