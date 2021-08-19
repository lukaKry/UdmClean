using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UdmClean.Application.DTOs.LeaveRequest;
using UdmClean.Application.Features.LeaveRequests.Requests.Queries;
using UdmClean.Application.Contracts.Persistance;
using UdmClean.Application.Contracts.Identity;
using UdmClean.Application.Contracts.Persistence;

namespace UdmClean.Application.Features.LeaveRequests.Handlers.Queries
{
    public class GetLeaveRequestDetailRequestHandler : IRequestHandler<GetLeaveRequestDetailRequest, LeaveRequestDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GetLeaveRequestDetailRequestHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<LeaveRequestDto> Handle(GetLeaveRequestDetailRequest request, CancellationToken cancellationToken)
        {
            var leaveRequest = _mapper.Map<LeaveRequestDto>(await _unitOfWork.LeaveRequestRepository.GetLeaveRequestWithDetailsAsync(request.Id));
            leaveRequest.Employee = await _userService.GetEmployee(leaveRequest.RequestingEmployeeId);
            return leaveRequest;
        }
    }
}
