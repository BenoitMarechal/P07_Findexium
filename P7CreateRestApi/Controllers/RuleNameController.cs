using Dot.Net.WebApi.Controllers.Domain;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Services;
using P7CreateRestApi.Repositories;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RuleNameController : ControllerBase
    {
        private readonly RuleNameService _service;
        private readonly ILogger<RuleNameController> _logger;

        public RuleNameController(RuleNameService service, ILogger<RuleNameController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // POST: api/v1/bidlist
        [HttpPost]
        public async Task<IActionResult> AddRuleName([FromBody] RuleName ruleName)
        {
            try
            {
                await _service.Add(ruleName);
                return CreatedAtAction(nameof(GetRuleName), new { id = ruleName.Id }, ruleName);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "A database error occurred while adding the RuleName.");
                return StatusCode(500, "A database error occurred while adding the RuleName.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }

        // GET: api/v1/bidlist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RuleName>>> GetAllRuleNames()
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
        public async Task<IActionResult> GetRuleName(int id)
        {
            try
            {
                var ruleName = await _service.GetById(id);
                return Ok(ruleName);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"RuleName with ID {id} not found.");
                return NotFound($"RuleName with ID {id} not found.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "A database error occurred while fetching the RuleName.");
                return StatusCode(500, "A database error occurred while fetching the RuleName.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }

        // PUT: api/v1/bidlist/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRuleName(int id, [FromBody] RuleName ruleName)
        {
            if (id != ruleName.Id)
            {
                return BadRequest("The ID from the route and the ID in the body do not match.");
            }

            try
            {
                await _service.Update(ruleName);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"RuleName with ID {id} not found.");
                return NotFound($"RuleName with ID {id} not found.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "A database error occurred while updating the RuleName.");
                return StatusCode(500, "A database error occurred while updating the RuleName.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }

        // DELETE: api/v1/bidlist/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRuleName(int id)
        {
            try
            {
                await _service.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"RuleName with ID {id} not found.");
                return NotFound($"RuleName with ID {id} not found.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "A database error occurred while deleting the RuleName.");
                return StatusCode(500, "A database error occurred while deleting the RuleName.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }
    }
}
