using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UdmClean.Application.DTOs.LeaveRequest.Validators;
using UdmClean.Application.Exceptions;
using UdmClean.Application.Features.LeaveRequests.Requests.Commands;
using UdmClean.Application.Contracts.Persistance;
using UdmClean.Application.Responses;
using UdmClean.Application.Contracts.Infrastructure;
using UdmClean.Application.Modules;
using UdmClean.Domain;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UdmClean.Application.Constants;
using UdmClean.Application.Contracts.Persistence;

namespace UdmClean.Application.Features.LeaveRequests.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailSender _emailSender;

        public CreateLeaveRequestCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IEmailSender emailSender)
        {
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveRequestDtoValidator(_unitOfWork.LeaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveRequestDto);
            var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(q => q.Type == CustomClaimTypes.Uid)?.Value;

            var allocation = await _unitOfWork.LeaveAllocationRepository.GetUserAllocations(userId, request.LeaveRequestDto.LeaveTypeId);
            if (allocation is null)
            {
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(request.LeaveRequestDto.LeaveTypeId), "You do not have any allocations for this leave type."));
            }
            else
            {
                int daysRequested = (int)(request.LeaveRequestDto.EndDate - request.LeaveRequestDto.StartDate).TotalDays;
                if (daysRequested > allocation.NumberOfDays)
                {
                    validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(request.LeaveRequestDto.EndDate), "You do not have enough days for this request."));
                }
            }

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Request Failed.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var leaveRequest = _mapper.Map<LeaveRequest>(request.LeaveRequestDto);
                leaveRequest.RequestingEmployeeId = userId;
                leaveRequest = await _unitOfWork.LeaveRequestRepository.AddAsync(leaveRequest);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = leaveRequest.Id;

                try
                {
                    var emailAddress = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;

                    var email = new Email
                    {
                        To = emailAddress,
                        Subject = "Leave Request Submitted",
                        Body = $"You leave request for {request.LeaveRequestDto.StartDate:D} to {request.LeaveRequestDto.EndDate:D} " +
                            $"has been submitted successfully."
                    };
                    await _emailSender.SendEmail(email);
                }
                catch (Exception)
                {
                    // catch error but not throw it... 
                }
            }

            return response;
        }
    }
}
