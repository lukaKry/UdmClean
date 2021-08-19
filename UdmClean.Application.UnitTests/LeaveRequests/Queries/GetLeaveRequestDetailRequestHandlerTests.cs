using AutoMapper;
using Microsoft.AspNetCore.Http;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UdmClean.Application.Contracts.Identity;
using UdmClean.Application.Contracts.Persistance;
using UdmClean.Application.Contracts.Persistence;
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
        private readonly IUnitOfWork _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserService _mockUserService;

        public GetLeaveRequestDetailRequestHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork().Object;
            _mockUserService = MockUserService.GetUserService().Object;

            var mapperConfiguration = new MapperConfiguration(p => p.AddProfile<MappingProfile>());
            _mapper = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task Handle_CalledWithExistingId_ReturnsLeaveRequestObject()
        {
            var handler = new GetLeaveRequestDetailRequestHandler(_mockUnitOfWork, _mapper, _mockUserService);

            var result = await handler.Handle(new GetLeaveRequestDetailRequest() { Id = 1 }, CancellationToken.None);

            result.ShouldBeOfType<LeaveRequestDto>();
            result.Id.ShouldBe(1);
        }
    }
}
