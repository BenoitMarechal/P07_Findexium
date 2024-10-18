using Dot.Net.WebApi.Controllers.Domain;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Services;
using P7CreateRestApi.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Dot.Net.WebApi.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class BidListController : ControllerBase
    {
        private readonly BidListService _service;
        private readonly ILogger<BidListController> _logger;

        public BidListController(BidListService service, ILogger<BidListController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // POST: api/v1/bidlist
        [HttpPost]
        public async Task<IActionResult> AddBidList([FromBody] BidList bidList)
        {
            try
            {
                await _service.Add(bidList);
                return CreatedAtAction(nameof(GetBidList), new { id = bidList.BidListId }, bidList);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "A database error occurred while adding the BidList.");
                return StatusCode(500, "A database error occurred while adding the BidList.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }

        // GET: api/v1/bidlist
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BidList>>> GetAllBidLists()
        {
            try
            {
                var result = await _service.GetAll();
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
        public async Task<IActionResult> GetBidList(int id)
        {
            try
            {
                var bidList = await _service.GetById(id);
                return Ok(bidList);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"BidList with ID {id} not found.");
                return NotFound($"BidList with ID {id} not found.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "A database error occurred while fetching the BidList.");
                return StatusCode(500, "A database error occurred while fetching the BidList.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }

        // PUT: api/v1/bidlist/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBidList(int id, [FromBody] BidList bidList)
        {
            if (id != bidList.BidListId)
            {
                return BadRequest("The ID from the route and the ID in the body do not match.");
            }

            try
            {
                await _service.Update(bidList);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"BidList with ID {id} not found.");
                return NotFound($"BidList with ID {id} not found.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "A database error occurred while updating the BidList.");
                return StatusCode(500, "A database error occurred while updating the BidList.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }

        // DELETE: api/v1/bidlist/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBidList(int id)
        {
            try
            {
                await _service.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"BidList with ID {id} not found.");
                return NotFound($"BidList with ID {id} not found.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "A database error occurred while deleting the BidList.");
                return StatusCode(500, "A database error occurred while deleting the BidList.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }
    }
}
