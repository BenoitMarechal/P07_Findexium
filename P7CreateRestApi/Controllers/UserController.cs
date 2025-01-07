using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Services;
using Microsoft.AspNetCore.Identity;
 using P7CreateRestApi.Models;
using P7CreateRestApi.Constants;
using Microsoft.AspNetCore.Authorization;


namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserRegistrationRequest registrationRequest)
        {
            _logger.LogInformation($"AddUser {registrationRequest.Username}");

            var user = new IdentityUser
            {
                UserName = registrationRequest.Username,
                Email = registrationRequest.Email,
                 
               
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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IdentityUser>>> GetAllUsers()
        {
            _logger.LogInformation($"GetAllUsers");
            try
            {
                var result = await _userManager.Users.ToListAsync();
                _logger.LogInformation("All users sucessfully fetched");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }

        
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            _logger.LogInformation($"GetUserById {id}");
            try
            {
                var user = await _userManager.FindByIdAsync(id);
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

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserPartial(string id, [FromBody] UpdateUserRequest request)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Update fields only if they are not null
            if (request.UserName != null) user.UserName = request.UserName;
            if (request.Email != null) user.Email = request.Email;
            if (request.PhoneNumber != null) user.PhoneNumber = request.PhoneNumber;
            if (request.EmailConfirmed.HasValue) user.EmailConfirmed = request.EmailConfirmed.Value;
            if (request.TwoFactorEnabled.HasValue) user.TwoFactorEnabled = request.TwoFactorEnabled.Value;
            if (request.LockoutEnabled.HasValue) user.LockoutEnabled = request.LockoutEnabled.Value;
            if (request.AccessFailedCount.HasValue) user.AccessFailedCount = request.AccessFailedCount.Value;

            // Save changes
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return StatusCode(500, "Failed to update the user.");
            }

            return Ok("User updated successfully.");
        }

        // DELETE: api/v1/user/{id}
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            _logger.LogInformation($"Delete User {id}");

            try
            {
                var targetUser = await _userManager.FindByIdAsync(id);
                await _userManager.DeleteAsync(targetUser);
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
