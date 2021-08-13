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
using UdmClean.Application.Features.LeaveTypes.Handlers.Queries;
using UdmClean.Application.Features.LeaveTypes.Requests.Queries;
using UdmClean.Application.Profiles;
using UdmClean.Application.UnitTests.Mocks;
using Xunit;

namespace UdmClean.Application.UnitTests.LeaveTypes.Queries
{
    public class GetLeaveTypeDetailRequestHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _mockRepo;
        public GetLeaveTypeDetailRequestHandlerTest()
        {
            _mockRepo = MockLeaveTypeRepository.GetLeaveTypeRepository().Object;

            var mapperConfiguration = new MapperConfiguration(p => p.AddProfile<MappingProfile>());
            _mapper = mapperConfiguration.CreateMapper();
        }
        
        [Fact]
        public async Task Handle_CalledWithExistingId_ReturnsLeaveTypeDtoObject()
        {
            var handler = new GetLeaveTypeDetailRequestHandler(_mockRepo, _mapper);

            var result = await handler.Handle(new GetLeaveTypeDetailRequest() { Id = 1 }, CancellationToken.None);

            result.ShouldBeOfType<LeaveTypeDto>();
            result.Id.ShouldBe(1);
        }

        [Fact]
        public async Task Handle_CalledWithoutIdParameter_ThrowsException()
        {
            // brak reakcji na taki przypadek w metodzie Handle()
        }

        [Fact]
        public async Task Handle_CalledWithNonExistingId_ThrowsException()
        {
            // this case also isn't covered yet
        }
    }
}
