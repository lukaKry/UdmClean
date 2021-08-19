using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdmClean.Application.DTOs.LeaveAllocation;
using UdmClean.Application.Features.LeaveAllocations.Requests.Commands;
using UdmClean.Application.Features.LeaveAllocations.Requests.Queries;
using UdmClean.Application.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UdmClean.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveAllocationController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // GET: api/<LeaveAllocationController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveAllocationDto>>> Get(bool isLoggedInUser = false)
        {
            var leaveAllocations = await _mediator.Send(new GetLeaveAllocationListRequest() { IsLoggedInUser = isLoggedInUser});
            return Ok(leaveAllocations);
        }

        // GET api/<LeaveAllocationController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveAllocationDto>> Get(int id)
        {
            var leaveAllocation = await _mediator.Send(new GetLeaveAllocationDetailRequest() { Id = id });
            return Ok(leaveAllocation);

        }

        // POST api/<LeaveAllocationController>
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateLeaveAllocationDto createLeaveAllocationDto)
        {
            var response = await _mediator.Send(new CreateLeaveAllocationCommand() { LeaveAllocationDto = createLeaveAllocationDto });
            return Ok(response);
        }

        // PUT api/<LeaveAllocationController>/5
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateLeaveAllocationDto updateLeaveAllocationDto)
        {
            await _mediator.Send(new UpdateLeaveAllocationCommand() { LeaveAllocationDto = updateLeaveAllocationDto });
            return NoContent();
        }

        // DELETE api/<LeaveAllocationController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteLeaveAllocationCommand() { Id = id });
            return NoContent();
        }
    }
}
