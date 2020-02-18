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
        
        //[HttpPost]
        //[Route("{id}/follow")]
        //public async Task<IActionResult> Follow(string id, int i) {
        //    //If user not logged in 
        //    if (!CookieHandler.LoggedIn(Request) || !Guid.TryParse(Request.Cookies["userId"].ToString(), out var UserId))
        //        return Unauthorized();

        //    //If user not found
        //    if (!await _userRepository.FollowUser(Guid.NewGuid(), id))
        //        return NotFound();

        //    //TODO return correct status code
        //    return RedirectToAction("UserTimeline", "Timeline");
        //}

        //[HttpPost]
        //[Route("{id}/unfollow")]
        //public async Task<IActionResult> Unfollow(string id) {
        //    //If user not logged in 
        //    if (!CookieHandler.LoggedIn(Request) || !Guid.TryParse(Request.Cookies["userId"].ToString(), out var UserId))
        //        return Unauthorized();

        //    //If user not found
        //    if (! await _userRepository.UnfollowUser(Guid.NewGuid(), id))
        //        return NotFound();

        //    //TODO return correct status code
        //    return Ok();
        //}

        [HttpPost]
        [Route("fllws/{username}")]
        public async Task<IActionResult> Follow([FromBody]followModel follow, string username)
        {
            //TODO maybe auth
            //If user not logged in 
            //if (!CookieHandler.LoggedIn(Request) || !Guid.TryParse(Request.Cookies["userId"].ToString(), out var UserId))
            //    return Unauthorized();

            if (string.IsNullOrEmpty(follow.follow) && !await _userRepository.UnfollowUser(Guid.NewGuid(), username))
                return NotFound();
            else if(string.IsNullOrEmpty(follow.follow) && !await _userRepository.FollowUser(Guid.NewGuid(), username))
                return NotFound();

            return NoContent();
        }

        //[HttpPost]
        //[Route("fllws/{username}")]
        //public async Task<IActionResult> UnFollow([FromBody]unFollowModel unfollow, string username)
        //{
        //    //If user not logged in 
        //    //if (!CookieHandler.LoggedIn(Request) || !Guid.TryParse(Request.Cookies["userId"].ToString(), out var UserId))
        //    //    return Unauthorized();

        //    //If user not found
        //    if (!await _userRepository.UnfollowUser(Guid.NewGuid(), username))
        //        return NotFound();

        //    //TODO return correct status code
        //    return RedirectToAction("UserTimeline", "Timeline");
        //}
    }
    public class followModel
    {
        public string follow { get; set; }
        public string unfollow { get; set; }
    }

    public class unFollowModel
    {
    }
}
