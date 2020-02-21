using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Minitwit.API.Util;
using System;
using System.IO;
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

        [LatestFilter]
        [HttpPost]
        [Route("fllws/{username}")]
        public async Task<IActionResult> fllws([FromBody]followModel follow, string username)
        {
            //TODO maybe auth
            //If user not logged in 
            //if (!CookieHandler.LoggedIn(Request) || !Guid.TryParse(Request.Cookies["userId"].ToString(), out var UserId))
            //    return Unauthorized();

            if (string.IsNullOrEmpty(follow.follow))
            {
                if (! _userRepository.UnfollowUser(username, follow.unfollow))
                    return NoContent();
            }
            else if (string.IsNullOrEmpty(follow.unfollow))
            {
                if (! _userRepository.FollowUser(username, follow.follow))
                    return NotFound();
            }
            return NoContent();
        }
    }
    public class followModel
    {
        public string follow { get; set; }
        public string unfollow { get; set; }
    }
}
