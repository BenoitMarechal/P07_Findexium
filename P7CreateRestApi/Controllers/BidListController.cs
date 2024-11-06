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
            _logger.LogInformation($"AddBidList {bidList.BidListId}");
            try
            {
                await _service.Add(bidList);
                _logger.LogInformation($"BidList {bidList.BidListId} sucessfully added");
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
            _logger.LogInformation($"GetAllBidLists");
            try
            {
                var result = await _service.GetAll();
                _logger.LogInformation($"Sucessfully fetched all BidLists");
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
        public async Task<IActionResult> GetBidList(string id)
        {
            _logger.LogInformation($"GetBidListById {id}");
            try
            {
                var bidList = await _service.GetById(id);
                _logger.LogInformation($"Sucessfully fetched BidList {bidList.BidListId}");
                return Ok(bidList);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"BidList with ID {id} not found.");
                return NotFound($"BidList with ID {id} not found.");
            }
           
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }

        // PUT: api/v1/bidlist/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBidList(string id, [FromBody] BidList bidList)
        {
            _logger.LogInformation($"UpdateBidList {id}");
            if (id != bidList.BidListId)
            {                
                _logger.LogError(Messages.NoMatchMessage);
                return BadRequest(Messages.NoMatchMessage);
            }

            try
            {
                await _service.Update(bidList);
                _logger.LogInformation($"Sucessfully updated BidList {bidList.BidListId}");
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
        public async Task<IActionResult> DeleteBidList(string id)
        {
            _logger.LogInformation($"Delete BidList {id}");
            try
            {
                await _service.Delete(id);
                _logger.LogInformation($"BidList {id} successfully deleted");
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
