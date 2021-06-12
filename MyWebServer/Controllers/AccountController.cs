using System;
using MyWebServer.Server.Controllers;
using MyWebServer.Server.Http;
using MyWebServer.Server.Results;

namespace MyWebServer.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(HttpRequest request) 
            : base(request)
        {
        }

        public ActionResult ActionWithCookies()
        {
            const string cookieName = "My-Cookie";

            if (this.Request.Cookies.ContainsKey(cookieName))
            {
                var cookie = this.Request.Cookies[cookieName];
                return Text($"Cookies already exist - {cookie}");
            }

            this.Response.AddCookie(cookieName, "My-cookie-value");
            this.Response.AddCookie("My-Second-Cookie", "My-second-cookie-value");


            return Text("Cookies set!");
        }
    }
}
