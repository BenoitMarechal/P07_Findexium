using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Services;
using Microsoft.AspNetCore.Identity;
 using P7CreateRestApi.Models;
using P7CreateRestApi.Constants;


namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly UserService _service;
        private readonly ILogger<UserController> _logger;

        public UserController(UserService service, ILogger<UserController> logger, UserManager<IdentityUser> userManager)
        {
            _service = service;
            _logger = logger;
            _userManager = userManager;
        }

        // Your user registration method
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserRegistrationRequest registrationRequest)
        {
            _logger.LogInformation($"AddUser {registrationRequest.Username}");

            var user = new IdentityUser
            {
                UserName = registrationRequest.Username,
                Email = registrationRequest.Email // Optional
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequest.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation($"User {user.UserName} sucessfully created");
                    return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
                }

                return BadRequest(result.Errors);
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
    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IdentityUser>>> GetAllUsers()
        {
            _logger.LogInformation($"GetAllUsers");
            try
            {
                var result = await _service.GetAll();
                _logger.LogInformation("All users sucessfully fetched");
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
            _logger.LogInformation($"GetUserById {id}");
            try
            {
                var user = await _service.GetById(id);
                _logger.LogInformation($"User {user.UserName} sucessfully fetched");
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"User with ID {id} not found.");
                return NotFound($"User with ID {id} not found.");
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
            _logger.LogInformation($"UpdateUser {id}");

            if (id != int.Parse(user.Id))
              //  if (id != user.Id)

            {
                _logger.LogError(Messages.NoMatchMessage);
                return BadRequest(Messages.NoMatchMessage);
            }

            try
            {
                await _service.Update(user);
                _logger.LogInformation($"User {user.UserName} sucessfully updated");
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
            _logger.LogInformation($"Delete User {id}");
            try
            {
                await _service.Delete(id);
                _logger.LogInformation($"User {id} successfully deleted");
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
