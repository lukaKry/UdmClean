using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UdmClean.Application.DTOs.LeaveAllocation;
using UdmClean.Application.Features.LeaveAllocations.Requests.Queries;
using UdmClean.Application.Contracts.Persistance;
using Microsoft.AspNetCore.Http;
using UdmClean.Application.Contracts.Identity;
using UdmClean.Domain;
using UdmClean.Application.Constants;
using UdmClean.Application.Contracts.Persistence;

namespace UdmClean.Application.Features.LeaveAllocations.Handlers.Queries
{
    public class GetLeaveAllocationListRequestHandler : IRequestHandler<GetLeaveAllocationListRequest, List<LeaveAllocationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public GetLeaveAllocationListRequestHandler(
            IUnitOfWork unitOfWork, 
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationListRequest request, CancellationToken cancellationToken)
        {
            var leaveAllocations = new List<LeaveAllocation>();
            var allocations = new List<LeaveAllocationDto>();

            if (request.IsLoggedInUser)
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == CustomClaimTypes.Uid)?.Value;
                leaveAllocations = await _unitOfWork.LeaveAllocationRepository.GetLeaveAllocationsWithDetailsAsync(userId);

                var employee = await _userService.GetEmployee(userId);
                allocations = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
                foreach (var alloc in allocations)
                {
                    alloc.Employee = employee;
                }
            }
            else
            {
                leaveAllocations = await _unitOfWork.LeaveAllocationRepository.GetLeaveAllocationsWithDetails();
                allocations = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
                foreach (var req in allocations)
                {
                    req.Employee = await _userService.GetEmployee(req.EmployeeId);
                }
            }

            return allocations;
        }
    }
}
