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
            _logger.LogInformation($"AddRuleName {ruleName.Id}");
            try

            {
                await _service.Add(ruleName);
                _logger.LogInformation($"RuleName {ruleName.Id} sucessfully added");
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
            _logger.LogInformation($"GetAllRuleNames");
            try
            {
                var result = await _service.GetAll();
                _logger.LogInformation($"Sucessfully fetched all RuleNames");
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
        public async Task<IActionResult> GetRuleName(string id)
        {
            _logger.LogInformation($"GetRuleNameById {id}");
            try
            {
                var ruleName = await _service.GetById(id);
                _logger.LogInformation($"Sucessfully fetched RuleName {ruleName.Id}");
                return Ok(ruleName);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"RuleName with ID {id} not found.");
                return NotFound($"RuleName with ID {id} not found.");
            }
         
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }

        // PUT: api/v1/bidlist/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRuleName(string id, [FromBody] RuleName ruleName)
        {
            _logger.LogInformation($"UpdateRuleName {id}");
            if (id != ruleName.Id)
            {
                _logger.LogError(Messages.NoMatchMessage);
                return BadRequest(Messages.NoMatchMessage);
            }

            try
            {
                await _service.Update(ruleName);
                _logger.LogInformation($"Sucessfully updated RuleName {ruleName.Id}");
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
        public async Task<IActionResult> DeleteRuleName(string id)
        {
            _logger.LogInformation($"Delete RuleName {id}");
            try
            {
                await _service.Delete(id);
                _logger.LogInformation($"RuleName {id} successfully deleted");
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
