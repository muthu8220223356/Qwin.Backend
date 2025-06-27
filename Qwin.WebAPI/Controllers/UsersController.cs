using Azure;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Qwin.WebAPI.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Qwin.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly QwinDbContext dbContext;
        private readonly IConfiguration configuration;

        public UsersController(QwinDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }

        [AllowAnonymous]
        [Route("token")]
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequestDto dto)
        {
            int? a = 10;
            a = null;
            var tokenDto = new TokenDto();
            if (ModelState.IsValid)
            {
                var result = dbContext.Users.FirstOrDefault(p => p.UserName == dto.UserName && p.Password == dto.Password);
                if (result != null)
                {
                    var claims = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Sub, dto.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: configuration["JwtSettings:Issuer"],
                        audience: configuration["JwtSettings:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddHours(1),
                        signingCredentials: creds
                        );

                    string tokenKey = new JwtSecurityTokenHandler().WriteToken(token);
                    if (tokenKey == null)
                    {
                        return Unauthorized();
                    }

                    tokenDto.Token = tokenKey;

                    // LoginTokenResponse loginTokenResponse = new LoginTokenResponse() { Token = tokenKey };


                }
            }

            return Ok(tokenDto);
        }

        // [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var usersList = dbContext.Users.Include(p => p.Details).ToList();

            List<UserResponseDto> result = new List<UserResponseDto>();
            foreach (var item in usersList)
            {
                result.Add(new UserResponseDto()
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    Email = item.Email,
                    Password = item.Password,
                    Address = item.Details.Address,
                    DateOfBirth = item.Details.DateOfBirth,
                    FirstName = item.Details.FirstName,
                    LastName = item.Details.LastName
                });
            }

            return Ok(result);
        }

        [AllowAnonymous]
        [Route("timesheet/{id:int}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            return Ok();
        }

        [Route("role/{id:int}")]
        [AllowAnonymous]

        [HttpGet]
        public IActionResult GetRoleById(int id)
        {
            return Ok();
        }


        [HttpPost]
        public IActionResult Post(UserRequestDto dto)
        {
            //Adding in the parent table
            var user = new User() { UserName = dto.UserName, Password = dto.Password, Email = dto.Email };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            //Adding in child
            UserDetails userDetails = new UserDetails()
            {
                UserId = user.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Address = dto.Address,
                DateOfBirth = dto.DateOfBirth
            };
            dbContext.UserDetails.Add(userDetails);
            dbContext.SaveChanges();


            return Ok();
        }
    }
}
