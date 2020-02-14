using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Minitwit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimelineController : ControllerBase
    {
        private readonly ITimelineRepository _timelineRepository;

        public TimelineController(ITimelineRepository timelineRepository)
        {
            _timelineRepository = timelineRepository;
        }

        // GET: api/Timeline/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Timeline
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}