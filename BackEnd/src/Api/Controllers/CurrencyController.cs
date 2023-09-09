namespace Api.Controllers
{
    using Domain.Dtos;
    using Domain.Dtos.Currency;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CurrencyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Currency
        /// <summary>
        /// Get Currencies
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetCurrencyQuery query)
        {
            var result = await _mediator.Send(query);
            return result.Error is null ? Ok(result) : BadRequest(result);
        }

        // POST: api/Currency
        /// <summary>
        /// Add Currency
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddCurrencyCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Error is null ? Ok(result) : BadRequest(result);
        }

        // PUT: api/Currency
        /// <summary>
        /// Update Currency
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateCurrencyCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Error is null ? NoContent() : BadRequest(result);
        }

        // DELETE: api/Currency/5
        /// <summary>
        /// Delete Currency
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteCurrencyCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Error is null ? NoContent() : BadRequest(result);
        }
    }
}
