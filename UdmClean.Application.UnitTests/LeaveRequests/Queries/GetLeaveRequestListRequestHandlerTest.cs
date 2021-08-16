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
using UdmClean.Application.DTOs.LeaveRequest;
using UdmClean.Application.Features.LeaveAllocations.Handlers.Queries;
using UdmClean.Application.Features.LeaveAllocations.Requests.Queries;
using UdmClean.Application.Features.LeaveRequests.Handlers.Queries;
using UdmClean.Application.Features.LeaveRequests.Requests.Queries;
using UdmClean.Application.Profiles;
using UdmClean.Application.UnitTests.Mocks;
using Xunit;

namespace UdmClean.Application.UnitTests.LeaveRequests.Queries
{
    public class GetLeaveRequestListRequestHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly ILeaveRequestRepository _mockRequestRepo;
        public GetLeaveRequestListRequestHandlerTest()
        {
            _mockRequestRepo = MockLeaveRequestRepository.GetLeaveRequestRepository().Object;

            var mapperConfiguration = new MapperConfiguration(p => p.AddProfile<MappingProfile>());
            _mapper = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task Handle_WhenCalled_ReturnListOfLeaveRequestListDtoObjects()
        {
            var handler = new GetLeaveRequestListRequestHandler(_mockRequestRepo, _mapper);

            var result = await handler.Handle(new GetLeaveRequestListRequest(), CancellationToken.None);

            result.ShouldBeOfType<List<LeaveRequestListDto>>();

            result.Count.ShouldBe(2);
        }
    }
}
