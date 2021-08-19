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
using UdmClean.Application.Contracts.Persistence;

namespace UdmClean.Application.Features.LeaveAllocations.Handlers.Queries
{
    public class GetLeaveAllocationDetailRequestHandler : IRequestHandler<GetLeaveAllocationDetailRequest, LeaveAllocationDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLeaveAllocationDetailRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<LeaveAllocationDto> Handle(GetLeaveAllocationDetailRequest request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _unitOfWork.LeaveAllocationRepository.GetAsync(request.Id);
            return _mapper.Map<LeaveAllocationDto>(leaveAllocation);
        }
    }
}
