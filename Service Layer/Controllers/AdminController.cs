using DAL.Repository;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;

namespace Service_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepo<Admin> _adminRepo;
        private readonly IConfiguration _config;
        public AdminController(IAdminRepo<Admin> adminRepo,IConfiguration configuration) 
        {
            this._adminRepo = adminRepo;
            this._config = configuration;
        }

        [HttpPost]
        [Route("ValidateAdmin")]
        [Produces("application/json")]
		[Consumes("application/json")]
		//[AllowAnonymous]
		public IActionResult ValidateAdmin([FromBody] Admin admin)
        {
            var ads = _adminRepo.ValidateAdmin(admin);
            if (ads != null)
            {
                var token = GenerateToken(admin);
                //return Ok(new { Token = token });//200
                return Ok(token);
            }
            else
            {
                return Unauthorized();//401
            }
        }

        [NonAction]
        public string GenerateToken(Admin adminInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email,adminInfo.EmailId),
                new Claim(ClaimTypes.Role,adminInfo.Role)

            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims, expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
