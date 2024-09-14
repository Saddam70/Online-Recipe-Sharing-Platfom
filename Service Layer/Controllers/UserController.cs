using DAL.Repository;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Service_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
       
        private readonly IUserRepo<User> _userRepo;
		private readonly IConfiguration _config;
		public UserController(IUserRepo<User> UserRepo ,IConfiguration configuration)
        {
            this._userRepo = UserRepo;
          this._config = configuration;
        }
        
        [HttpPost]
        [Route("RegisterUser")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [AllowAnonymous]
        public IActionResult RegisterUser([FromBody] User user)
        {
            //also howing null in newuser
            var newUser = _userRepo.RegisterUser(user);
            if (newUser != null)
            {
                return Ok(newUser);//200
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("UpdateUser/{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        //[Authorize(Roles = "Customer")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            var cx = _userRepo.GetUserProfileById(id);
            if (cx != null)
            {
                _userRepo.UpdateUserProfile(user);
                return Accepted();//201
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("DeleteUser/{id}")]
        [Produces("application/json")]
        public IActionResult DeleteUser(int id)
        {
            var del = _userRepo.GetUserProfileById(id); // Retrieve the user by ID first
            if (del == null)
            {
                return NotFound(); // Return 404 if the user is not found
            }

            var deleted = _userRepo.DeleteUser(del); // Call to delete the user profile
            if (deleted != null)
            {
                return Ok(deleted); // Return 204 No Content if deletion is successful
            }
            else
            {
                return BadRequest(); // Return 400 Bad Request if the deletion fails
            }
        }
        [HttpPost]
        [Route("ValidateUser")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [AllowAnonymous]
        public IActionResult ValidateUser([FromBody] User user)
        {
            var cx = _userRepo.ValidateUser(user);
            if (cx != null)
            {
                var token = GenerateToken(user);
                return Ok(cx);//200
            }
            else
            {
                return Unauthorized();//401
            }
        }
        [NonAction]
        public string GenerateToken(User cx)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                      new Claim(ClaimTypes.Email,cx.EmailId),
                      new Claim(ClaimTypes.Role,cx.Role)

                  };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims, expires: DateTime.Now.AddMinutes(60), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet]
        [Route("GetAllUser")]
        [Produces("application/json")]
        public IActionResult GetUser()
        {
            var users = _userRepo.GetAllUsers();
            return Ok(users);
        }
        [HttpGet]
        [Route("GetUserById/{id}")]
        [Produces("application/json")]
        public IActionResult GetUserById(int id)
        {
            var users = _userRepo.GetUserProfileById(id);
            return Ok(users);
        }

    }
}
