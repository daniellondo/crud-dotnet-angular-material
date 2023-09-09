namespace Api.Controllers
{
    using Domain.Dtos.Branch;
    using Domain.Dtos.Currency;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BranchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Branch
        /// <summary>
        /// Get Branches
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetBranchQuery query)
        {
            var result = await _mediator.Send(query);
            return result.Error is null ? Ok(result) : BadRequest(result);
        }

        // POST: api/Branch
        /// <summary>
        /// Add Branch
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddBranchCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Error is null ? Ok(result) : BadRequest(result);
        }

        // PUT: api/Branch
        /// <summary>
        /// Update Branch
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateBranchCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Error is null ? NoContent() : BadRequest(result);
        }

        // DELETE: api/Branch/5
        /// <summary>
        /// Delete Branch
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteBranchCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Error is null ? NoContent() : BadRequest(result);
        }
    }
}
