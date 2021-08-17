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
using UdmClean.Application.Responses;
using UdmClean.Application.UnitTests.Mocks;
using Xunit;

namespace UdmClean.Application.UnitTests.LeaveTypes.Commands
{
    public class CreateLeaveAllocationCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _mockRepo;
        private readonly CreateLeaveTypeDto _createLeaveTypeDto;
        private readonly CreateLeaveTypeCommandHandler _handler;
        public CreateLeaveAllocationCommandHandlerTest()
        {
            _mockRepo = MockLeaveTypeRepository.GetLeaveTypeRepository().Object;

            var mapperConfig = new MapperConfiguration(p => p.AddProfile<MappingProfile>());
            _mapper = mapperConfig.CreateMapper();
            _createLeaveTypeDto = new CreateLeaveTypeDto()
            {
                Name = "name",
                DefaultDays = 1
            };

            _handler = new CreateLeaveTypeCommandHandler(_mockRepo, _mapper);

        }

        [Fact]
        public async Task Handle_ValidRequestData_ReturnsIdOfNewObject()
        {
            var leaveTypesCountBefore = _mockRepo.GetAllAsync().Result.Count;

            var result = await _handler.Handle(new CreateLeaveTypeCommand() { LeaveTypeDto = _createLeaveTypeDto }, CancellationToken.None);

            var leaveTypes = await _mockRepo.GetAllAsync();

            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeTrue();
            leaveTypes.Count.ShouldBe(leaveTypesCountBefore + 1);
        }

        [Fact]
        public async Task Handle_InvalidRequestData_ThrowsValidationException()
        {
            _createLeaveTypeDto.DefaultDays = -1;
            
            var leaveTypesBefore = await _mockRepo.GetAllAsync();

            var response = await _handler.Handle(new CreateLeaveTypeCommand() { LeaveTypeDto = _createLeaveTypeDto }, CancellationToken.None);

            var leaveTypesAfter= await _mockRepo.GetAllAsync();

            leaveTypesAfter.Count.ShouldBe(leaveTypesBefore.Count);

            response.Success.ShouldBeFalse();
        }
    }
}
