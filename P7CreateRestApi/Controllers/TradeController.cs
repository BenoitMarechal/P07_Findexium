using Dot.Net.WebApi.Controllers.Domain;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Services;
using P7CreateRestApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using P7CreateRestApi.Constants;

namespace Dot.Net.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TradeController : ControllerBase
    {
        private readonly TradeService _service;
        private readonly ILogger<TradeController> _logger;

        public TradeController(TradeService service, ILogger<TradeController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // POST: api/v1/bidlist
        [HttpPost]
        public async Task<IActionResult> AddTrade([FromBody] Trade trade)
        {
            _logger.LogInformation($"AddTrade {trade.TradeId}");
            try
            {
                await _service.Add(trade);
                _logger.LogInformation($"Trade {trade.TradeId} sucessfully added");
                return CreatedAtAction(nameof(GetTrade), new { id = trade.TradeId }, trade);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "A database error occurred while adding the Trade.");
                return StatusCode(500, "A database error occurred while adding the Trade.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }

        // GET: api/v1/bidlist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trade>>> GetAllTrades()
        {
            _logger.LogInformation($"GetAllTrades");
            try
            {
                var result = await _service.GetAll();
                _logger.LogInformation("All trades fetched successfully");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }

        // GET: api/v1/bidlist/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrade(string id)
        {
            _logger.LogInformation($"GetTradeById {id}");
            try
            {
                var trade = await _service.GetById(id);
                _logger.LogInformation($"Bidlists ${id} fetched successfully");
                return Ok(trade);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"Trade with ID {id} not found.");
                return NotFound($"Trade with ID {id} not found.");
            }
         
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }

        // PUT: api/v1/bidlist/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrade(string id, [FromBody] Trade trade)
        {
            _logger.LogInformation($"UpdateTrade {id}");
            if (id != trade.TradeId)
            {
                _logger.LogError(Messages.NoMatchMessage);
                return BadRequest(Messages.NoMatchMessage);
            }

            try
            {
                await _service.Update(trade);
                _logger.LogInformation($"Sucessfully updated Trade {trade.TradeId}");
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"Trade with ID {id} not found.");
                return NotFound($"Trade with ID {id} not found.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "A database error occurred while updating the Trade.");
                return StatusCode(500, "A database error occurred while updating the Trade.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }

        // DELETE: api/v1/bidlist/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrade(string id)
        {
            _logger.LogInformation($"Delete Trade {id}");
            try
            {
                await _service.Delete(id);
                _logger.LogInformation($"Trade {id} successfully deleted");
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"Trade with ID {id} not found.");
                return NotFound($"Trade with ID {id} not found.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "A database error occurred while deleting the Trade.");
                return StatusCode(500, "A database error occurred while deleting the Trade.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }
    }
}
