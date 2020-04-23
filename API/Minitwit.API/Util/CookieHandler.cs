using Microsoft.AspNetCore.Http;
using System;

namespace Minitwit.API.Util
{
    public class CookieHandler
    {
        public CookieOptions SetCookie(int? expireTime)
        {
            CookieOptions options = new CookieOptions();

            if (expireTime.HasValue)
                options.Expires = DateTime.UtcNow.AddMinutes(expireTime.Value);
            else
                options.Expires = DateTime.UtcNow.AddMilliseconds(100);

            return options;
        }

        public static bool LoggedIn(HttpRequest req)
        {
            var cookieUser = req.Cookies["user"];
            var cookieUserId = req.Cookies["userId"];

            return (cookieUser != null) && (cookieUserId != null);
            //if (cookieUser == null) return false;
            //return true;
        }

        public static bool LoggedIn(IRequestCookieCollection req)
        {
            var cookieUser = req["user"];
            var cookieUserId = req["userId"];

            return (cookieUser != null) && (cookieUserId != null);
        }
    }

}
