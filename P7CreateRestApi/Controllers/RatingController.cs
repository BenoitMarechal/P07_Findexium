using Dot.Net.WebApi.Controllers.Domain;
using Dot.Net.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Services;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RatingController : ControllerBase
    {        
        private readonly LocalDbContext _context;
        private readonly RatingService _service;

        public RatingController(LocalDbContext context,RatingService service)
        {
            _context = context;
            _service = service;
        }      


        [HttpPost]
        public async Task<IActionResult> AddRating([FromBody]Rating rating)
        {
            await _service.Add(rating);
            return CreatedAtAction(nameof(GetRating), new { id= rating.Id}, rating);

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rating>>> GetAllRatings()
        {  
            var result= await _service.GetAll();
            return Ok(result); 
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetRating(int id)
        {
            var rating=await _service.GetById(id);


            return Ok(rating);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateRating(int id, [FromBody] Rating rating)
        {
            if (id != rating.Id)
            {
                return BadRequest("Ids from route and body do not match");
            }
            await _service.Update(rating);

            return Ok(rating);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            await _service.Delete(id);

            return NoContent();
        }


        private bool RatingExists(int id)
        {
            return _context.Ratings.Any(e => e.Id == id);
        }

    }
}