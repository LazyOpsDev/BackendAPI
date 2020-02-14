using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Minitwit.API.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        [HttpPost]
        [Route("follow")]
        public IActionResult Follow() {
            return Created("TODO", "TODO");
        }

        [HttpPost]
        [Route("unfollow")]
        public IActionResult Unfollow() {
            return Ok("");
        }
    }
}
