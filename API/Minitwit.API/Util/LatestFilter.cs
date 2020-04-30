using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Minitwit.API.Util
{
    public class LatestFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var a = context.HttpContext.Request.Query["latest"].FirstOrDefault()?.ToString(); //HttpContext.Items["latest"];
            if (int.TryParse(a, out var latest))
            {
                context.HttpContext.RequestServices.GetService<ILastNumberRepository>().WriteLatest(latest);
            }
            base.OnActionExecuting(context);
        }
    }
}
