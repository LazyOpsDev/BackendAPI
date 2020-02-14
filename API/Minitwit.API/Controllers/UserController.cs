using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Minitwit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

    }
}
