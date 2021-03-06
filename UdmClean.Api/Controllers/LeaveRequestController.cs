using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdmClean.Application.DTOs.LeaveRequest;
using UdmClean.Application.Features.LeaveRequests.Requests.Commands;
using UdmClean.Application.Features.LeaveRequests.Requests.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UdmClean.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<LeaveRequestController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestListDto>>> Get()
        {
            var leaveRequests = await _mediator.Send(new GetLeaveRequestListRequest());
            return Ok(leaveRequests);
        }


        // GET api/<LeaveRequestController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequestDto>> Get(int id)
        {
            var leaveRequest = await _mediator.Send(new GetLeaveRequestDetailRequest() { Id = id });
            return Ok(leaveRequest);
        }

        // POST api/<LeaveRequestController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CreateLeaveRequestDto createLeaveRequestDto)
        {
            var response = await _mediator.Send(new CreateLeaveRequestCommand() { LeaveRequestDto = createLeaveRequestDto });
            return Ok(response.Id);
        }

        // PUT api/<LeaveRequestController>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateLeaveRequestDto updateLeaveRequestDto)
        {
            await _mediator.Send(new UpdateLeaveRequestCommand() { UpdateLeaveRequestDto = updateLeaveRequestDto });
            return NoContent();
        }

        // PUT api/<LeaveRequestController>/changeApproval/5
        [HttpPut("changeApproval/{id}")]
        public async Task<ActionResult> ChangeApproval(int id, bool approvalStatus)
        {
            await _mediator.Send(new UpdateLeaveRequestCommand() 
            { 
                ChangeLeaveRequestAprovalDto = new ChangeLeaveRequestAprovalDto() 
                { 
                    Id = id, Approved = approvalStatus 
                } 
            });

            return NoContent();
        }

        // DELETE api/<LeaveRequestController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteLeaveRequestCommand() { Id = id });
            return NoContent();
        }
    }
}
