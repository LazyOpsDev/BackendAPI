using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Minitwit.API.Controllers;
using Minitwit.API.Util;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Minitwit.Tests.Controllers
{
    public class UserControllerTests
    {
        [Fact]
        public async Task FllwsRejectsUnauthorizedRequestsWith401Error()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var loggerMock = new Mock<ILogger<UserController>>();
            var httpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext(); // or mock a `HttpContext`
            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };

            var controller = new UserController(userRepositoryMock.Object, loggerMock.Object) { ControllerContext = controllerContext };

            var res = await controller.fllws(new followModel(), "John Doe");

            Assert.IsType<UnauthorizedResult>(res);
        }

        [Fact]
        public async Task FllwsExecutesRequestMadeWithSimulatorHeader()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var loggerMock = new Mock<ILogger<UserController>>();
            var httpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext(); // or mock a `HttpContext`
            httpContext.Request.Headers["Authorization"] = AuthorizationConstants.terribleHackAuth;
            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };

            var controller = new UserController(userRepositoryMock.Object, loggerMock.Object) { ControllerContext = controllerContext };

            var res = await controller.fllws(new followModel(), "John Doe");

            Assert.IsType<NoContentResult>(res);
        }

    }
}
