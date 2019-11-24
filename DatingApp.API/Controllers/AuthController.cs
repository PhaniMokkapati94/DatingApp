using System;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Dtos;
using DatingApp.API.Interfaces;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers {

    [Route ("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController (IAuthRepository repo, IConfiguration config) {
            _config = config;
            _repo = repo;

        }

        [HttpPost ("register")]
        public async Task<IActionResult> Register (UserforRegisterDTO userdto) {
            userdto.username = userdto.username.ToLower ();

            if (await _repo.UserExists (userdto.username)) return BadRequest ("User Already Exists");

            var UsertoCreate = new User {
                Username = userdto.username
            };
            var CreatedUser = await _repo.Register (UsertoCreate, userdto.password);

            return StatusCode (201);

        }

        [HttpPost ("Login")]
        public async Task<IActionResult> Login (UserforLoginDTO userforLoginDto) {
            var userfromrepo = await _repo.Login (userforLoginDto.username.ToLower(), userforLoginDto.password);
            if (userfromrepo == null) return Unauthorized ();

            var claims = new [] {

                new Claim (ClaimTypes.NameIdentifier, userfromrepo.Id.ToString ()),
                new Claim (ClaimTypes.Name, userfromrepo.Username)
            };

            var key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (_config.GetSection ("AppSettings:Token").Value));
            var cred = new SigningCredentials (key, SecurityAlgorithms.HmacSha512Signature);

            var TokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity (claims),
                SigningCredentials = cred,
                Expires = DateTime.Now.AddDays (1)

            };
            var tokenhandler = new JwtSecurityTokenHandler ();
            var token = tokenhandler.CreateToken (TokenDescriptor);

            return Ok (new {
                token = tokenhandler.WriteToken (token)
            });

        }

    }
}