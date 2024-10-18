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
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly RatingService _service;
        private readonly ILogger<RatingController> _logger;

        public RatingController(RatingService service, ILogger<RatingController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // POST: api/v1/bidlist
        [HttpPost]
        public async Task<IActionResult> AddRating([FromBody] Rating rating)
        {
            try
            {
                await _service.Add(rating);
                return CreatedAtAction(nameof(GetRating), new { id = rating.Id }, rating);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "A database error occurred while adding the Rating.");
                return StatusCode(500, "A database error occurred while adding the Rating.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }

        // GET: api/v1/bidlist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rating>>> GetAllRatings()
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
        public async Task<IActionResult> GetRating(int id)
        {
            try
            {
                var rating = await _service.GetById(id);
                return Ok(rating);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"Rating with ID {id} not found.");
                return NotFound($"Rating with ID {id} not found.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "A database error occurred while fetching the Rating.");
                return StatusCode(500, "A database error occurred while fetching the Rating.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }

        // PUT: api/v1/bidlist/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRating(int id, [FromBody] Rating rating)
        {
            if (id != rating.Id)
            {
                return BadRequest("The ID from the route and the ID in the body do not match.");
            }

            try
            {
                await _service.Update(rating);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"Rating with ID {id} not found.");
                return NotFound($"Rating with ID {id} not found.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "A database error occurred while updating the Rating.");
                return StatusCode(500, "A database error occurred while updating the Rating.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }

        // DELETE: api/v1/bidlist/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            try
            {
                await _service.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"Rating with ID {id} not found.");
                return NotFound($"Rating with ID {id} not found.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "A database error occurred while deleting the Rating.");
                return StatusCode(500, "A database error occurred while deleting the Rating.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }
    }
}
