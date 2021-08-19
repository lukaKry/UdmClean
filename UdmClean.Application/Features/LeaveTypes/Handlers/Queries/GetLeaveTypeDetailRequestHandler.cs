using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UdmClean.Application.DTOs.LeaveType;
using UdmClean.Application.Features.LeaveTypes.Requests.Queries;
using UdmClean.Application.Contracts.Persistance;
using UdmClean.Application.Contracts.Persistence;

namespace UdmClean.Application.Features.LeaveTypes.Handlers.Queries
{
    public class GetLeaveTypeDetailRequestHandler : IRequestHandler<GetLeaveTypeDetailRequest, LeaveTypeDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLeaveTypeDetailRequestHandler(
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<LeaveTypeDto> Handle(GetLeaveTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var leaveType = await _unitOfWork.LeaveTypeRepository.GetAsync(request.Id);
            return _mapper.Map<LeaveTypeDto>(leaveType);
        }
    }
}
