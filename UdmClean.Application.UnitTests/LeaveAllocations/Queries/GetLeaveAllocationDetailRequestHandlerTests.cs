using AutoMapper;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UdmClean.Application.Contracts.Persistance;
using UdmClean.Application.DTOs.LeaveAllocation;
using UdmClean.Application.Features.LeaveAllocations.Handlers.Queries;
using UdmClean.Application.Features.LeaveAllocations.Requests.Queries;
using UdmClean.Application.Profiles;
using UdmClean.Application.UnitTests.Mocks;
using Xunit;

namespace UdmClean.Application.UnitTests.LeaveAllocations.Queries
{
    public class GetLeaveRequestDetailRequestHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _mockRepo;
        public GetLeaveRequestDetailRequestHandlerTests()
        {
            _mockRepo = MockLeaveAllocationRepository.GetLeaveAllocationRepository().Object;

            var mapperConfiguration = new MapperConfiguration(p => p.AddProfile<MappingProfile>());
            _mapper = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task Handle_CalledWithExistingId_ReturnsLeaveAllocationObject()
        {
            var handler = new GetLeaveAllocationDetailRequestHandler(_mockRepo, _mapper);

            var result = await handler.Handle(new GetLeaveAllocationDetailRequest() { Id = 1 }, CancellationToken.None);

            result.ShouldBeOfType<LeaveAllocationDto>();
            result.Id.ShouldBe(1);
        }
    }
}
