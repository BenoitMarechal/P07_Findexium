using Microsoft.AspNetCore.Identity; 
 using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using P7CreateRestApi.Models;
using System.Net;
using P7CreateRestApi.Constants;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace P7CreateRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager; 
        private readonly ILogger<LoginController> _logger;

        public LoginController(IConfiguration config, UserManager<IdentityUser> userManager, ILogger<LoginController> logger)
        {
            _config = config;
            _userManager = userManager; 
            _logger = logger;
    }

        [HttpPost]        
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]

        public async Task<IActionResult> Post([FromBody] LoginRequest loginRequest)
        {
           

            _logger.LogInformation($"Trying to log user {loginRequest.Username} in");
            try
            {
                var user = await _userManager.FindByNameAsync(loginRequest.Username); 
                if (user == null)
                {
                    // User not found
                    _logger.LogError(Messages.BadLoginMessage);
                    return Unauthorized(Messages.BadLoginMessage); 
                }

                // Verify the password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
                if (!passwordCheck)
                {
                    // Invalid password
                    _logger.LogError(Messages.BadLoginMessage);
                    return Unauthorized(Messages.BadLoginMessage); 
                }

                var authClaims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    };
                var roles = await _userManager.GetRolesAsync(user);

                authClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
                // Generate the token
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                   audience: _config["Jwt:Issuer"],                    
                    claims: authClaims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials);

                var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

                _logger.LogInformation($"User {user.UserName} successfully logged in");

                // Return the token
                return Ok(new { Token = token }); 
            }
            catch (Exception e) {
                _logger.LogError(e, e.Message);
                return BadRequest();
            }


           
        }
    }
}
