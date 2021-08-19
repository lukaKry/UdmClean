using AutoMapper;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using UdmClean.Application.Contracts.Persistence;
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
        private readonly IUnitOfWork _mockUnitOfWork;
        private readonly IMapper _mapper;
        public GetLeaveRequestDetailRequestHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork().Object;

            var mapperConfiguration = new MapperConfiguration(p => p.AddProfile<MappingProfile>());
            _mapper = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task Handle_CalledWithExistingId_ReturnsLeaveAllocationObject()
        {
            var handler = new GetLeaveAllocationDetailRequestHandler(_mockUnitOfWork, _mapper);

            var result = await handler.Handle(new GetLeaveAllocationDetailRequest() { Id = 1 }, CancellationToken.None);

            result.ShouldBeOfType<LeaveAllocationDto>();
            result.Id.ShouldBe(1);
        }
    }
}
