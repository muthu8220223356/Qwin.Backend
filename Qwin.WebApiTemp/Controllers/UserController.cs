using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qwin.WebApiTemp.User.DTOs;
using Qwin.WebApiTemp.User.Entity;

namespace Qwin.WebApiTemp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILogger<UserController> logger;

        public UserController(IMapper mapper,ILogger<UserController> logger)
        {
            logger.LogInformation("UserController is entered");
            this.mapper = mapper;
            this.logger = logger;
        }
        [HttpPost]
        public IActionResult Post(UserDto dto)
        {
            logger.LogInformation("Post is entered");
            try
            {
                int a = 10;
                int b = a / 0;
            }
            catch (Exception ex)
            {
                logger.LogError(dto.Name, ex);
            }
            var result = mapper.Map<UserEntity>(dto);
            return Ok(result);
        }
    }
}
