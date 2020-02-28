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
        //[HttpGet]
        //public async Task<IActionResult> Root()
        //{
        //    // If not logged in redirect to public,
        //    //if (!CookieHandler.LoggedIn(Request) || !Guid.TryParse(Request.Cookies["userId"].ToString(), out var UserId))
        //    //    return await Public();

        //    // if logged in get timeline consisting of messages the user follows
        //    // TODO get logged in user
        //    return new OkObjectResult(await _timelineRepository.GetPublicTimeline());
        //}

    [LatestFilter]
        [HttpGet]
        [Route("/")]
        public async Task<IActionResult> Public()
        {
            //Return all messages by all users
            return new OkObjectResult( _timelineRepository.GetPublicTimeline());
        }

    [LatestFilter]
        [HttpGet]
        [Route("{username}")]
        public async Task<IActionResult> UserTimeline(string username)
        {
            //Return messages by a single user
            return new OkObjectResult( _timelineRepository.GetUserTimeline(username));
        }

    [LatestFilter]
        [HttpPost]
        [HttpGet]
        [Route("msgs/{username}")]
        public async Task<IActionResult> AddMessage([FromBody]MessageCreate msg, string username) {
            //Create a new message from logged in user
            //TODO if user not logged in
            //if (!CookieHandler.LoggedIn(Request) || !Guid.TryParse(Request.Cookies["userId"].ToString(), out var UserId))
            //    return Unauthorized();

            switch (Request.Method)
            {
                case "POST":
                     _timelineRepository.PostMessage(username, msg.content);
                    return NoContent();
                case "GET":
                     _timelineRepository.GetUserTimeline(username);
                    return NoContent();
            }

            
            return NoContent();
        }

        public class MessageCreate
        {
            public string content { get; set; }
        }
    }
}