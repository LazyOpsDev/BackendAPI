using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [LatestFilter]
        [HttpPost]
        [Route("fllws/{username}")]
        public async Task<IActionResult> fllws([FromBody]followModel follow, string username)
        {
            //TODO maybe auth
            //If user not logged in 
            if (!CookieHandler.LoggedIn(Request) &&
                !(Request.Headers.TryGetValue("Authorization", out var header) && header.Equals(AuthorizationConstants.terribleHackAuth)))
                return Unauthorized();

            // TODO: Figure out this mess :)
            if (string.IsNullOrEmpty(follow.follow))
            {
                _logger.LogInformation($"User {username} Unfollow user {follow.unfollow}");
                if (! _userRepository.UnfollowUser(username, follow.unfollow))
                    return NoContent();
            }
            else if (string.IsNullOrEmpty(follow.unfollow))
            {
                _logger.LogInformation($"User {username} follow user {follow.follow}"); 
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
