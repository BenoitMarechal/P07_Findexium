using Dot.Net.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dot.Net.WebApi.Domain;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BidListController : ControllerBase
    {
        // TODO: Inject BidList service
        private readonly LocalDbContext _context;

        public BidListController(LocalDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddBidList([FromBody] BidList bidList)
        {
            _context.BidLists.Add(bidList);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBidList), new { id = bidList.BidListId }, bidList);

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<BidList>>> GetAllBidLists()
        {
            return await _context.BidLists.ToListAsync();
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBidList(int id)
        {
            // TODO: find all BidList, add to model
            var bidList = await _context.BidLists.FindAsync(id);
            if (bidList == null)
            {
                return NotFound();
            }
            return Ok(bidList);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateBidList(int id, [FromBody] BidList bidList)
        {
            if (id != bidList.BidListId)
            {
                return BadRequest();
            }

            _context.Entry(bidList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BidListExists(id))
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
        public async Task<IActionResult> DeleteBidList(int id)
        {
            var bidList = await _context.BidLists.FindAsync(id);
            if (bidList == null)
            {
                return NotFound();
            }

            _context.BidLists.Remove(bidList);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool BidListExists(int id)
        {
            return _context.BidLists.Any(e => e.BidListId == id);
        }

    }
}