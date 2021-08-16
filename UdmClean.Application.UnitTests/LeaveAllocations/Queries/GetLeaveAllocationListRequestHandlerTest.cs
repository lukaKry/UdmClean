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
    public class GetLeaveRequestListRequestHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _mockRepo;
        public GetLeaveRequestListRequestHandlerTest()
        {
            _mockRepo = MockLeaveAllocationRepository.GetLeaveAllocationRepository().Object;

            var mapperConfiguration = new MapperConfiguration(p => p.AddProfile<MappingProfile>());
            _mapper = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task Handle_WhenCalled_ReturnListOfLeaveAllocationTypeDtoObjects()
        {
            var handler = new GetLeaveAllocationListRequestHandler(_mockRepo, _mapper);

            var result = await handler.Handle(new GetLeaveAllocationListRequest(), CancellationToken.None);

            result.ShouldBeOfType<List<LeaveAllocationDto>>();

            result.Count.ShouldBe(2);
        }
    }
}
