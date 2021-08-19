using AutoMapper;
using Microsoft.AspNetCore.Http;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UdmClean.Application.Contracts.Identity;
using UdmClean.Application.Contracts.Persistence;
using UdmClean.Application.DTOs.LeaveRequest;
using UdmClean.Application.Features.LeaveRequests.Handlers.Queries;
using UdmClean.Application.Features.LeaveRequests.Requests.Queries;
using UdmClean.Application.Profiles;
using UdmClean.Application.UnitTests.Mocks;
using Xunit;

namespace UdmClean.Application.UnitTests.LeaveRequests.Queries
{
    public class GetLeaveRequestListRequestHandlerTest
    {
        private readonly IUnitOfWork _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserService _mockUserService;
        private readonly IHttpContextAccessor _mockAccessor;
        public GetLeaveRequestListRequestHandlerTest()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork().Object;

            var mapperConfiguration = new MapperConfiguration(p => p.AddProfile<MappingProfile>());
            _mapper = mapperConfiguration.CreateMapper();

            _mockUserService = MockUserService.GetUserService().Object;

            _mockAccessor = MockHttpContextAccessor.GetHttpContextAccessor().Object;
        }

        [Fact]
        public async Task Handle_WhenCalled_ReturnListOfLeaveRequestListDtoObjects()
        {
            var handler = new GetLeaveRequestListRequestHandler(_mockUnitOfWork, _mapper, _mockUserService, _mockAccessor);

            var result = await handler.Handle(new GetLeaveRequestListRequest(), CancellationToken.None);

            result.ShouldBeOfType<List<LeaveRequestListDto>>();

            result.Count.ShouldBe(2);
        }
    }
}
