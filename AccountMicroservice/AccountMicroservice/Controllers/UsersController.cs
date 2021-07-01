using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AccountMicroservice.Models;
using AccountMicroservice.Repository;
using Microsoft.AspNetCore.Authorization;

namespace AccountMicroservice.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        readonly IUserRepository UserRepository;

        public UsersController(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        [HttpPost("/token")]
        public IActionResult Token(string email, string password)
        {
            var identity = GetIdentity(email, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid email or password." });
            }

            var now = DateTime.UtcNow;
            
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                email = identity.Name
            };

            return Json(response);
        }

        private ClaimsIdentity GetIdentity(string email, string password)
        {
            User user = UserRepository.GetByLoginForm(email, password);

            if (user != null)
            {
                string role = "user";
                if (user.IsAdmin)
                {
                    role = "admin";
                }
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }

        [Authorize]
        [HttpGet(Name = "GetAllUsers")]
        public IEnumerable<User> Get()
        {
            return UserRepository.Get();
        }

        [Authorize]
        [HttpGet("{Id}", Name = "GetUser")]
        public IActionResult Get(int Id)
        {
            User user = UserRepository.Get(Id);

            if (user == null)
            {
                return NotFound();
            }

            return new ObjectResult(user);
        }

        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            user.IsAdmin = false;
            UserRepository.Create(user);
            User userFromBD = UserRepository.Get(user.Id);
            if (userFromBD != null)
            {
                return CreatedAtRoute("GetUser", new { id = user.Id }, user);
            }
            else
            {
                return BadRequest(new { errorText = "User with this email is already registered" });
            }

        }

        [Authorize]
        [HttpPut("{Id}")]
        public IActionResult Update(int Id, [FromBody] User updatedUser)
        {
            if (updatedUser == null || updatedUser.Id != Id)
            {
                return BadRequest();
            }

            var user = UserRepository.Get(Id);
            if (user == null)
            {
                return NotFound();
            }

            UserRepository.Update(updatedUser);
            user = UserRepository.Get(Id);
            return new ObjectResult(user);
        }

        [Authorize]
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var deletedUser = UserRepository.Delete(Id);

            if (deletedUser == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedUser);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("changeRole/{Id}", Name = "ChangeRole")]
        public IActionResult ChangeRole(int Id)
        {

            var user = UserRepository.Get(Id);
            if (user == null)
            {
                return NotFound();
            }

            UserRepository.ChangeRole(Id);
            user = UserRepository.Get(Id);
            return new ObjectResult(user);
        }

    }
}