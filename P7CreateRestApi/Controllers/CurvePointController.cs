
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Services;
using Microsoft.AspNetCore.Authorization;
using P7CreateRestApi.Constants;

namespace Dot.Net.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CurvePointController : ControllerBase
    {
        private readonly CurvePointService _service;
        private readonly ILogger<CurvePointController> _logger;

        public CurvePointController(CurvePointService service, ILogger<CurvePointController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // POST: api/v1/bidlist
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddCurvePoint([FromBody] CurvePoint curvePoint)
        {
            _logger.LogInformation($"AddCurvePoint {curvePoint.Id}");
            try
            {
                await _service.Add(curvePoint);
                _logger.LogInformation($"CurvePoint {curvePoint.Id} sucessfully added");
                return CreatedAtAction(nameof(GetCurvePoint), new { id = curvePoint.Id }, curvePoint);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "A database error occurred while adding the CurvePoint.");
                return StatusCode(500, "A database error occurred while adding the CurvePoint.");
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
        public async Task<ActionResult<IEnumerable<CurvePoint>>> GetAllCurvePoints()
        {
            _logger.LogInformation($"GetAllCurvePoints");
            try
            {
                var result = await _service.GetAll();
                _logger.LogInformation($"Sucessfully fetched all CurvePoints");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }

        // GET: api/v1/bidlist/{id}
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCurvePoint(int id)
        {
            _logger.LogInformation($"GetCurvePointById {id}");
            try
            {
                var curvePoint = await _service.GetById(id);
                _logger.LogInformation($"Sucessfully fetched CurvePoint {curvePoint.Id}");
                return Ok(curvePoint);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"CurvePoint with ID {id} not found.");
                return NotFound($"CurvePoint with ID {id} not found.");
            }
            
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }

        // PUT: api/v1/bidlist/{id}
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCurvePoint(int id, [FromBody] CurvePoint curvePoint)
        {
            _logger.LogInformation($"UpdateCurvePoint {id}");

            if (id != curvePoint.Id)
            {
                _logger.LogError(Messages.NoMatchMessage);
                return BadRequest(Messages.NoMatchMessage);
            }

            try
            {
                await _service.Update(curvePoint);
                _logger.LogInformation($"Sucessfully updated CurvePoint {curvePoint.Id}");
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"CurvePoint with ID {id} not found.");
                return NotFound($"CurvePoint with ID {id} not found.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "A database error occurred while updating the CurvePoint.");
                return StatusCode(500, "A database error occurred while updating the CurvePoint.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }

        // DELETE: api/v1/bidlist/{id}
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurvePoint(int id)
        {
            _logger.LogInformation($"Delete CurvePoint {id}");
            try
            {
                await _service.Delete(id);
                _logger.LogInformation($"CurvePoint {id} successfully deleted");
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"CurvePoint with ID {id} not found.");
                return NotFound($"CurvePoint with ID {id} not found.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "A database error occurred while deleting the CurvePoint.");
                return StatusCode(500, "A database error occurred while deleting the CurvePoint.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }
    }
}
