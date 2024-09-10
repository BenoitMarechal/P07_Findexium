using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurveController : ControllerBase
    {
        // TODO: Inject Curve Point service
        private readonly LocalDbContext _context;

        public CurveController(LocalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<IEnumerable<CurvePoint>>> Home()
        {
            return await _context.CurvePoints.ToListAsync();
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddCurvePoint([FromBody]CurvePoint curvePoint)
        {
            return Ok();
        }

        [HttpPut]
        [Route("validate")]
        public IActionResult Validate([FromBody]CurvePoint curvePoint)
        {
            // TODO: check data valid and save to db, after saving return bid list
            return Ok();
        }

        [HttpGet]
        [Route("update/{id}")]
        public IActionResult ShowUpdateForm(int id)
        {
            // TODO: get CurvePoint by Id and to model then show to the form
            return Ok();
        }

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult UpdateCurvePoint(int id, [FromBody] CurvePoint curvePoint)
        {
            // TODO: check required fields, if valid call service to update Curve and return Curve list
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteBid(int id)
        {
            // TODO: Find Curve by Id and delete the Curve, return to Curve list
            return Ok();
        }
    }
}