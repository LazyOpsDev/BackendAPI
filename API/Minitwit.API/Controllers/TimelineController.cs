using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Root()
        {
            // If not logged in redirect to public,
            // if logged in get timeline consisting of messages the user follows
            return Ok("Called /");
        }

        [HttpGet]
        [Route("public")]
        public IActionResult Public()
        {
            //Return all messages by all users
            return Ok("Called /public");
        }

        [HttpGet]
        [Route("{username}")]
        public IActionResult Private(string username)
        {
            //Return messages by a single user
            return Ok($"Called /{username}");
        }

        [HttpPost]
        [Route("add_message")]
        public IActionResult AddMessage(string tweet) {
            //Create a new message from logged in user
            return Created("TODO", "TODO");
        }


    }
}