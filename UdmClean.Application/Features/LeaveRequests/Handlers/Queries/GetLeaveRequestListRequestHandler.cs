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
using Microsoft.AspNetCore.Http;
using UdmClean.Application.Contracts.Identity;
using UdmClean.Domain;
using UdmClean.Application.Constants;
using UdmClean.Application.Contracts.Persistence;

namespace UdmClean.Application.Features.LeaveRequests.Handlers.Queries
{
    public class GetLeaveRequestListRequestHandler : IRequestHandler<GetLeaveRequestListRequest, List<LeaveRequestListDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetLeaveRequestListRequestHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IUserService userService,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListRequest request, CancellationToken cancellationToken)
        {
            var leaveRequests = new List<LeaveRequest>();
            var requests = new List<LeaveRequestListDto>();

            if (request.IsLoggedInUser)
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == CustomClaimTypes.Uid)?.Value;
                leaveRequests = await _unitOfWork.LeaveRequestRepository.GetLeaveRequestsWithDetailsAsync(userId);

                var employee = await _userService.GetEmployee(userId);
                requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
                foreach (var req in requests)
                {
                    req.Employee = employee;
                }
            }
            else
            {
                leaveRequests = await _unitOfWork.LeaveRequestRepository.GetLeaveRequestsWithDetailsAsync();
                requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
                foreach (var req in requests)
                {
                    req.Employee = await _userService.GetEmployee(req.RequestingEmployeeId);
                }
            }

            return requests;
        }
    }
}
