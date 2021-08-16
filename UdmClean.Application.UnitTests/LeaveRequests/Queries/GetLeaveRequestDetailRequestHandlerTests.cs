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
using UdmClean.Application.Features.LeaveRequests.Handlers.Queries;
using UdmClean.Application.Features.LeaveRequests.Requests.Queries;
using UdmClean.Application.Profiles;
using UdmClean.Application.UnitTests.Mocks;
using Xunit;

namespace UdmClean.Application.UnitTests.LeaveRequests.Queries
{
    public class GetLeaveRequestDetailRequestHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly ILeaveRequestRepository _mockRequestRepo;
        public GetLeaveRequestDetailRequestHandlerTests()
        {
            _mockRequestRepo = MockLeaveRequestRepository.GetLeaveRequestRepository().Object;

            var mapperConfiguration = new MapperConfiguration(p => p.AddProfile<MappingProfile>());
            _mapper = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task Handle_CalledWithExistingId_ReturnsLeaveRequestObject()
        {
            var handler = new GetLeaveRequestDetailRequestHandler(_mockRequestRepo, _mapper);

            var result = await handler.Handle(new GetLeaveRequestDetailRequest() { Id = 1 }, CancellationToken.None);

            result.ShouldBeOfType<LeaveRequestDto>();
            result.Id.ShouldBe(1);
        }
    }
}
