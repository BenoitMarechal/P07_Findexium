using Dot.Net.WebApi.Controllers.Domain;
using Dot.Net.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RatingController : ControllerBase
    {
        // TODO: Inject Rating service
        private readonly LocalDbContext _context;

        public RatingController(LocalDbContext context)
        {
            _context = context;
        }      

        [HttpPost]
        public async Task<IActionResult> AddRating([FromBody]Rating rating)
        {
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRating), new { id= rating.Id}, rating);

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rating>>> GetAllRatings()
        {  
            return await _context.Ratings.ToListAsync();
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetRating(int id)
        {
            // TODO: find all Rating, add to model
            var rating= await _context.Ratings.FindAsync(id);
            if(rating == null)
            {
                return NotFound();
            }
            return Ok(rating);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateRating(int id, [FromBody] Rating rating)
        {
            if (id != rating.Id)
            {
                return BadRequest();
            }

            _context.Entry(rating).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(id))
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
        public async Task<IActionResult> DeleteRating(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool RatingExists(int id)
        {
            return _context.Ratings.Any(e => e.Id == id);
        }

    }
}