using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UdmClean.Application.Contracts.Infrastructure;
using UdmClean.Application.Contracts.Persistance;
using UdmClean.Application.DTOs.LeaveRequest;
using UdmClean.Application.Features.LeaveRequests.Handlers.Commands;
using UdmClean.Application.Features.LeaveRequests.Requests.Commands;
using UdmClean.Application.Profiles;
using UdmClean.Application.Responses;
using UdmClean.Application.UnitTests.Mocks;
using Xunit;

namespace UdmClean.Application.UnitTests.LeaveRequests.Commands
{
    public class CreateLeaveRequestCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly ILeaveRequestRepository _mockRequestRepo;
        private readonly CreateLeaveRequestDto _createLeaveRequestDto;
        private readonly CreateLeaveRequestCommandHandler _handler;
        public CreateLeaveRequestCommandHandlerTest()
        {
            _mockRequestRepo = MockLeaveRequestRepository.GetLeaveRequestRepository().Object;

            var mapperConfig = new MapperConfiguration(p => p.AddProfile<MappingProfile>());
            _mapper = mapperConfig.CreateMapper();
            _createLeaveRequestDto = new CreateLeaveRequestDto()
            {
                StartDate = DateTime.Now - new TimeSpan(2, 0, 0, 0, 0),
                EndDate = DateTime.Now + new TimeSpan(2, 0, 0, 0, 0),
                LeaveTypeId = 1,
                RequestComments = "create"
            };

            var mockTypeRepository = MockLeaveTypeRepository.GetLeaveTypeRepository().Object;

            var sender = new Mock<IEmailSender>().Object;


            _handler = new CreateLeaveRequestCommandHandler(_mockRequestRepo, mockTypeRepository, sender, _mapper);

        }

        [Fact]
        public async Task Handle_ValidRequestData_ReturnsSuccessfulBaseCommandResponse()
        {
            var result = await _handler.Handle(new CreateLeaveRequestCommand() { LeaveRequestDto = _createLeaveRequestDto }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeTrue();
        }

        [Fact]
        public async Task Handle_InvalidRequestData_ReturnsFailedBaseCommandResponse()
        {
            _createLeaveRequestDto.StartDate = DateTime.Now + new TimeSpan(2, 0, 0, 0, 0);
            _createLeaveRequestDto.EndDate = DateTime.Now - new TimeSpan(2, 0, 0, 0, 0);

            var result = await _handler.Handle(new CreateLeaveRequestCommand() { LeaveRequestDto = _createLeaveRequestDto }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeFalse();
        }
    }
}
