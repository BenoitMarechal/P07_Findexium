using Dot.Net.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dot.Net.WebApi.Domain;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RuleNameController : ControllerBase
    {
        // TODO: Inject RuleName service
        private readonly LocalDbContext _context;

        public RuleNameController(LocalDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddRuleName([FromBody] RuleName ruleName)
        {
            _context.RuleNames.Add(ruleName);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRuleName), new { id = ruleName.Id }, ruleName);

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<RuleName>>> GetAllRuleNames()
        {
            return await _context.RuleNames.ToListAsync();
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetRuleName(int id)
        {
            // TODO: find all RuleName, add to model
            var ruleName = await _context.RuleNames.FindAsync(id);
            if (ruleName == null)
            {
                return NotFound();
            }
            return Ok(ruleName);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateRuleName(int id, [FromBody] RuleName ruleName)
        {
            if (id != ruleName.Id)
            {
                return BadRequest();
            }

            _context.Entry(ruleName).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RuleNameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRuleName(int id)
        {
            var ruleName = await _context.RuleNames.FindAsync(id);
            if (ruleName == null)
            {
                return NotFound();
            }

            _context.RuleNames.Remove(ruleName);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool RuleNameExists(int id)
        {
            return _context.RuleNames.Any(e => e.Id == id);
        }

    }
}