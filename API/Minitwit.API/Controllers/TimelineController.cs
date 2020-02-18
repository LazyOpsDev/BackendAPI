using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Minitwit.API.Util;
using System;
using System.Threading.Tasks;

namespace Minitwit.API.Controllers
{
    [Route("/")]
    [ApiController]
    public class TimelineController : ControllerBase
    {
        private readonly ITimelineRepository _timelineRepository;

        public TimelineController(ITimelineRepository timelineRepository)
        {
            _timelineRepository = timelineRepository;
        }
        
        // GET: api/Timeline
        [HttpGet]
        public async Task<IActionResult> Root()
        {
            // If not logged in redirect to public,
            if (!CookieHandler.LoggedIn(Request) || !Guid.TryParse(Request.Cookies["userId"].ToString(), out var UserId))
                return await Public();

            // if logged in get timeline consisting of messages the user follows
            // TODO get logged in user
            return new OkObjectResult(await _timelineRepository.GetTimelineForLoggedInUser(UserId));
        }

        [HttpGet]
        [Route("public")]
        public async Task<IActionResult> Public()
        {
            //Return all messages by all users
            return new OkObjectResult(await _timelineRepository.GetPublicTimeline());
        }

        [HttpGet]
        [Route("{username}")]
        public async Task<IActionResult> UserTimeline(string username)
        {
            //Return messages by a single user
            return new OkObjectResult(await _timelineRepository.GetUserTimeline(username));
        }

        [HttpPost]
        [Route("add_message")]
        public async Task<IActionResult> AddMessage(string tweet) {
            //Create a new message from logged in user
            //TODO if user not logged in
            if (!CookieHandler.LoggedIn(Request) || !Guid.TryParse(Request.Cookies["userId"].ToString(), out var UserId))
                return Unauthorized();

            //TODO get logged in user
            await _timelineRepository.PostMessage(UserId, tweet);
            
            return await Root();
        }


    }
}