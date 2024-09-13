using Dot.Net.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dot.Net.WebApi.Domain;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TradeController : ControllerBase
    {
        // TODO: Inject Trade service
        private readonly LocalDbContext _context;

        public TradeController(LocalDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddTrade([FromBody] Trade trade)
        {
            _context.Trades.Add(trade);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTrade), new { id = trade.TradeId }, trade);

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trade>>> GetAllTrades()
        {
            return await _context.Trades.ToListAsync();
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTrade(int id)
        {
            // TODO: find all Trade, add to model
            var trade = await _context.Trades.FindAsync(id);
            if (trade == null)
            {
                return NotFound();
            }
            return Ok(trade);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTrade(int id, [FromBody] Trade trade)
        {
            if (id != trade.TradeId)
            {
                return BadRequest();
            }

            _context.Entry(trade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TradeExists(id))
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
        public async Task<IActionResult> DeleteTrade(int id)
        {
            var trade = await _context.Trades.FindAsync(id);
            if (trade == null)
            {
                return NotFound();
            }

            _context.Trades.Remove(trade);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool TradeExists(int id)
        {
            return _context.Trades.Any(e => e.TradeId == id);
        }

    }
}