using Dot.Net.WebApi.Controllers.Domain;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Services;
using P7CreateRestApi.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        private readonly ILogger<UserController> _logger;

        public UserController(UserService service, ILogger<UserController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // POST: api/v1/user
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] IdentityUser user)
        {
            try
            {
                await _service.Add(user);
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "A database error occurred while adding the User.");
                return StatusCode(500, "A database error occurred while adding the User.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }

        // GET: api/v1/user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IdentityUser>>> GetAllUsers()
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

        // GET: api/v1/user/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var user = await _service.GetById(id);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"User with ID {id} not found.");
                return NotFound($"User with ID {id} not found.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "A database error occurred while fetching the User.");
                return StatusCode(500, "A database error occurred while fetching the User.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }

        // PUT: api/v1/user/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] IdentityUser user)
        {
            if (id != int.Parse(user.Id))
            {
                return BadRequest("The ID from the route and the ID in the body do not match.");
            }

            try
            {
                await _service.Update(user);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"User with ID {id} not found.");
                return NotFound($"User with ID {id} not found.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "A database error occurred while updating the User.");
                return StatusCode(500, "A database error occurred while updating the User.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }

        // DELETE: api/v1/user/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _service.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"User with ID {id} not found.");
                return NotFound($"User with ID {id} not found.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "A database error occurred while deleting the User.");
                return StatusCode(500, "A database error occurred while deleting the User.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }
    }
}
