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
            _logger.LogInformation($"AddRating {rating.Id}");
            try
            {
                await _service.Add(rating);
                _logger.LogInformation($"Rating {rating.Id} sucessfully added");
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
            _logger.LogInformation($"GetAllRatings");
            try
            {
                var result = await _service.GetAll();
                _logger.LogInformation($"Sucessfully fetched all Ratings");
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
        public async Task<IActionResult> GetRating(string id)
        {
            _logger.LogInformation($"GetRatingById {id}");
            try
            {
                var rating = await _service.GetById(id);
                _logger.LogInformation($"Sucessfully fetched Rating {rating.Id}");
                return Ok(rating);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"Rating with ID {id} not found.");
                return NotFound($"Rating with ID {id} not found.");
            }
        
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }

        // PUT: api/v1/bidlist/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRating(string id, [FromBody] Rating rating)
        {
            _logger.LogInformation($"UpdateRating {id}");
            if (id != rating.Id)
            {
                _logger.LogError(Messages.NoMatchMessage);
                return BadRequest(Messages.NoMatchMessage);
            }

            try
            {
                await _service.Update(rating);
                _logger.LogInformation($"Sucessfully updated Rating {rating.Id}");
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
        public async Task<IActionResult> DeleteRating(string id)
        {
            _logger.LogInformation($"Delete Rating {id}");
            try
            {
                await _service.Delete(id);
                _logger.LogInformation($"Rating {id} successfully deleted");
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
