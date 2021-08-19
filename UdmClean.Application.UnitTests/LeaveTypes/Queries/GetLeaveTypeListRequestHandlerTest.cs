using AutoMapper;
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
using UdmClean.Application.Features.LeaveTypes.Handlers.Queries;
using UdmClean.Application.Features.LeaveTypes.Requests.Queries;
using UdmClean.Application.Profiles;
using UdmClean.Application.UnitTests.Mocks;
using Xunit;

namespace UdmClean.Application.UnitTests.LeaveTypes.Queries
{
    public class GetLeaveTypeListRequestHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _mockUow;
        public GetLeaveTypeListRequestHandlerTest()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork().Object;

            var mapperConfiguration = new MapperConfiguration(p => p.AddProfile<MappingProfile>());
            _mapper = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task Handle_WhenCalled_ReturnListOfLeaveTypeDtoObjects()
        {
            var handler = new GetLeaveTypeListRequestHandler(_mockUow, _mapper);

            var result = await handler.Handle(new GetLeaveTypeListRequest(), CancellationToken.None);

            result.ShouldBeOfType<List<LeaveTypeDto>>();

            result.Count.ShouldBe(2);
        }
    }
}
