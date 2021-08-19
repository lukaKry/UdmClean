﻿using AutoMapper;
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
using UdmClean.Application.Features.LeaveTypes.Handlers.Commands;
using UdmClean.Application.Features.LeaveTypes.Requests.Commands;
using UdmClean.Application.Profiles;
using UdmClean.Application.UnitTests.Mocks;
using Xunit;

namespace UdmClean.Application.UnitTests.LeaveTypes.Commands
{
    public class UpdateLeaveAllocationCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _mockUow;
        private readonly UpdateLeaveTypeCommandHandler _handler;
        private LeaveTypeDto _leaveTypeDto;

        public UpdateLeaveAllocationCommandHandlerTest()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork().Object;

            var mapperConfig = new MapperConfiguration( c => c.AddProfile<MappingProfile>());
            _mapper = mapperConfig.CreateMapper();


            _leaveTypeDto = new LeaveTypeDto()
            {
                Id = 1,
                Name = "Name",
                DefaultDays = 1
            };

            _handler = new UpdateLeaveTypeCommandHandler(_mockUow, _mapper);
        }

        [Fact]
        public async Task Handle_ValidCommandData_UpdatesRecordInRepository()
        {
            await _handler.Handle(new UpdateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto }, CancellationToken.None);

            var result = await _mockUow.LeaveTypeRepository.GetAsync(1);

            result.Name.ShouldBe("Name");
            result.DefaultDays.ShouldBe(1);
        }
    }
}
