using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Minitwit.API.Controllers;
using Minitwit.API.Util;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Minitwit.Tests
{
    public class TimelineControllerTests
    {
        [Fact]
        public async Task AddMessageRejectsUnauthorizedRequestsWith401Error()
        {
            var timelineRepositoryMock = new Mock<ITimelineRepository>();
            var loggerMock = new Mock<ILogger<TimelineController>>();
            var httpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext(); // or mock a `HttpContext`
            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };

            var controller = new TimelineController(timelineRepositoryMock.Object, loggerMock.Object) { ControllerContext = controllerContext };

            var res = await controller.AddMessage(new TimelineController.MessageCreate { content = "hello" }, "testUser");

            Assert.IsType<UnauthorizedResult>(res);
        }

        [Fact]
        public async Task AddMessageExecutesRequestMadeWithSimulatorHeader()
        {
            var timelineRepositoryMock = new Mock<ITimelineRepository>();
            var loggerMock = new Mock<ILogger<TimelineController>>();
            var httpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext(); // or mock a `HttpContext`
            httpContext.Request.Headers["Authorization"] = AuthorizationConstants.terribleHackAuth;
            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };

            var controller = new TimelineController(timelineRepositoryMock.Object, loggerMock.Object) { ControllerContext = controllerContext };

            var res = await controller.AddMessage(new TimelineController.MessageCreate { content = "hello" }, "testUser");

            Assert.IsType<NoContentResult>(res);
        }
    }
}
