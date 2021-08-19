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
    public class GetLeaveTypeListRequestHandler : IRequestHandler<GetLeaveTypeListRequest, List<LeaveTypeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLeaveTypeListRequestHandler(
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypeListRequest request, CancellationToken cancellationToken)
        {
            var leaveTypes = await _unitOfWork.LeaveTypeRepository.GetAllAsync();
            return _mapper.Map<List<LeaveTypeDto>>(leaveTypes);
        }
    }
}
