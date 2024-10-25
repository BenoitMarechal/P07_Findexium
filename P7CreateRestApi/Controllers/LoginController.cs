using Microsoft.AspNetCore.Identity; // Add this if not already present
 using Microsoft.AspNetCore.Mvc;
// using Auth0.ManagementApi.Models.Rules;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using P7CreateRestApi.Models;
using System.Net; // Make sure to import your LoginRequest model
// Other usings...

namespace P7CreateRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager; // Inject UserManager
        private readonly ILogger<LoginController> _logger;

        public LoginController(IConfiguration config, UserManager<IdentityUser> userManager, ILogger<LoginController> logger)
        {
            _config = config;
            _userManager = userManager; // Initialize UserManager
            _logger = logger;
    }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]

        public async Task<IActionResult> Post([FromBody] LoginRequest loginRequest)
        {
            // Find user by username or email

            try
            {
                var user = await _userManager.FindByNameAsync(loginRequest.Username); // Or use FindByEmailAsync
                if (user == null)
                {
                    return Unauthorized("Incorrect user or password"); // User not found
                }

                // Verify the password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
                if (!passwordCheck)
                {
                    return Unauthorized("Incorrect user or password"); // Invalid password
                }

                // Generate the token
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new JwtSecurityToken(
                    _config["Jwt:Issuer"],
                    _config["Jwt:Issuer"],
                    null,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials);

                var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

                return Ok(new { Token = token }); // Return the token }
            }
            catch (Exception e) {
                _logger.LogError(e, e.Message);
                return BadRequest();
            }


           
        }
    }
}
