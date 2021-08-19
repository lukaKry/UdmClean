using AutoMapper;
using Microsoft.AspNetCore.Http;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UdmClean.Application.Contracts.Identity;
using UdmClean.Application.Contracts.Persistence;
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
        public async Task Handle_WhenCalled_ReturnListOfLeaveAllocationTypeDtoObjects()
        {
            var handler = new GetLeaveAllocationListRequestHandler(_mockUnitOfWork, _mapper, _mockAccessor, _mockUserService);

            var result = await handler.Handle(new GetLeaveAllocationListRequest(), CancellationToken.None);

            result.ShouldBeOfType<List<LeaveAllocationDto>>();

            result.Count.ShouldBe(2);
        }
    }
}
