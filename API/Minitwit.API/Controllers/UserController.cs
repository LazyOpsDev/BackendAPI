using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Minitwit.API.Controllers
{
    [Route("/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        [HttpPost]
        [Route("{id}/follow")]
        public IActionResult Follow(Guid id) {
            //follow user with id
            return Created("TODO", "TODO");
        }

        [HttpPost]
        [Route("{id}/unfollow")]
        public IActionResult Unfollow(Guid id) {
            //Unfollow user
            return Ok("");
        }
    }
}
