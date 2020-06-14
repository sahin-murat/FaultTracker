using FaultTracker.Business.DataTransfer.Request;
using FaultTracker.Business.Interfaces;
using FaultTracker.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FaultTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UnitOfWork _UoW;

        public AuthController(IConfiguration configuration, IUnitOfWork unitOfWork )
        {
            _configuration = configuration;
            _UoW = unitOfWork as UnitOfWork;
        }

        [HttpGet]
        [Route("Test")]
        public async Task<IActionResult> Test()
        {
            var items = _UoW.Users.GetAll();
            return Ok(items);
        }

        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserRequestDto user)
        {
            //Add claims for user 
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, user.ID.ToString()),
                new Claim("Name", user.FirstName ),
                new Claim("LastName", user.LastName )
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
