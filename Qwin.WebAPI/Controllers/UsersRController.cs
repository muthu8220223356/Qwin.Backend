using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qwin.WebAPI.Repository;

namespace Qwin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersRController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UsersRController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task< IActionResult> Get()
        {
            var users = await userRepository.GetUsersAsync();
            return Ok(users);
        }
    }
}
