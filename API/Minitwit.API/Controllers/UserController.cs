using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Minitwit.API.Util;
using System;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Follow(string id) {
            //If user not logged in 
            if (!CookieHandler.LoggedIn(Request) || !Guid.TryParse(Request.Cookies["userId"].ToString(), out var UserId))
                return Unauthorized();

            //If user not found
            if (!await _userRepository.FollowUser(Guid.NewGuid(), id))
                return NotFound();

            //TODO return correct status code
            return RedirectToAction("UserTimeline", "Timeline");
        }

        [HttpPost]
        [Route("{id}/unfollow")]
        public async Task<IActionResult> Unfollow(string id) {
            //If user not logged in 
            if (!CookieHandler.LoggedIn(Request) || !Guid.TryParse(Request.Cookies["userId"].ToString(), out var UserId))
                return Unauthorized();

            //If user not found
            if (! await _userRepository.UnfollowUser(Guid.NewGuid(), id))
                return NotFound();

            //TODO return correct status code
            return Ok();
        }
    }
}
