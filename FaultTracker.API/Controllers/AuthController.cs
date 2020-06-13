using FaultTracker.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FaultTracker.API.Controllers
{

    public class userObj { public int id { get; set; } public string name { get; set; } public string surname { get; set; } }

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DatabaseContext _databaseContext;

        public AuthController(IConfiguration configuration, DatabaseContext databaseContext)
        {
            _configuration = configuration;
            _databaseContext = databaseContext;
        }        

        [HttpGet]
        [Route("Test")]
        public async Task<IActionResult> Test()
        {
            var users = _databaseContext.User.ToList();

            return Ok(users);
        }

        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] userObj user)
        {
            //Add claims for user 
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, user.id.ToString()),
                new Claim("Name", user.name ),
                new Claim("Surname", user.surname )
            };

            //Create Key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthenticationOptions:Secret"]));
            //Decide algoritm
            var algorithm = SecurityAlgorithms.HmacSha256;

            //Prepare credentials
            var signingCredentials = new SigningCredentials(key, algorithm);

            var notBefore = DateTime.Now;
            var expires = notBefore.AddMinutes(60);

            //Prepare token 
            var token = new JwtSecurityToken(
                _configuration["AuthenticationOptions:Issuer"],
                _configuration["AuthenticationOptions:Audience"],
                claims,
                notBefore: notBefore,
                expires: expires,
                signingCredentials);

            //Create acces token 
            var accesToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { AccesToken = accesToken, Expires = expires });
        }

        [Authorize]
        [HttpPost]
        [Route("ValidateToken")]
        public async Task<IActionResult> ValidateToken()
        {
            return Ok(new { Success = true, Message = "Access token is valid." });
        }
    }
}
