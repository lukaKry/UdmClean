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
using UdmClean.Application.Features.LeaveTypes.Handlers.Commands;
using UdmClean.Application.Features.LeaveTypes.Requests.Commands;
using UdmClean.Application.Profiles;
using UdmClean.Application.UnitTests.Mocks;
using Xunit;

namespace UdmClean.Application.UnitTests.LeaveTypes.Commads
{
    public class UpdateLeaveTypeCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _mockRepo;
        private readonly UpdateLeaveTypeCommandHandler _handler;
        private LeaveTypeDto _leaveTypeDto;

        public UpdateLeaveTypeCommandHandlerTest()
        {
            var mapperConfig = new MapperConfiguration( c => c.AddProfile<MappingProfile>());
            _mapper = mapperConfig.CreateMapper();

            _mockRepo = MockLeaveTypeRepository.GetLeaveTypeRepository().Object;

            _leaveTypeDto = new LeaveTypeDto()
            {
                Id = 1,
                Name = "Name",
                DefaultDays = 1
            };

            _handler = new UpdateLeaveTypeCommandHandler(_mockRepo, _mapper);
        }

        [Fact]
        public async Task Handle_ValidCommandData_UpdatesRecordInRepository()
        {
            await _handler.Handle(new UpdateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto }, CancellationToken.None);

            var result = await _mockRepo.GetAsync(1);

            result.Name.ShouldBe("Name");
            result.DefaultDays.ShouldBe(1);
        }
    }
}
