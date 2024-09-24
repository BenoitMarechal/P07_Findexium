using Dot.Net.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dot.Net.WebApi.Domain;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CurvePointController : ControllerBase
    {
        // TODO: Inject CurvePoint service
        private readonly LocalDbContext _context;

        public CurvePointController(LocalDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddCurvePoint([FromBody] CurvePoint curvePoint)
        {
            _context.CurvePoints.Add(curvePoint);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCurvePoint), new { id = curvePoint.Id }, curvePoint);

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CurvePoint>>> GetAllCurvePoints()
        {
            return await _context.CurvePoints.ToListAsync();
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCurvePoint(int id)
        {
            // TODO: find all CurvePoint, add to model
            var curvePoint = await _context.CurvePoints.FindAsync(id);
            if (curvePoint == null)
            {
                return NotFound();
            }
            return Ok(curvePoint);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCurvePoint(int id, [FromBody] CurvePoint curvePoint)
        {
            if (id != curvePoint.Id)
            {
                return BadRequest();
            }

            _context.Entry(curvePoint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurvePointExists(id))
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
        public async Task<IActionResult> DeleteCurvePoint(int id)
        {
            var curvePoint = await _context.CurvePoints.FindAsync(id);
            if (curvePoint == null)
            {
                return NotFound();
            }

            _context.CurvePoints.Remove(curvePoint);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool CurvePointExists(int id)
        {
            return _context.CurvePoints.Any(e => e.Id == id);
        }

    }
}