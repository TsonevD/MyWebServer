using System;
using MyWebServer.Server.Controllers;
using MyWebServer.Server.Http;
using MyWebServer.Server.Results;

namespace MyWebServer.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(HttpRequest request)
            : base(request)
        {
        }
        public HttpResponse Index()
            => Text("Hello from Dimitar!");

        public HttpResponse toSoftUni() 
            => Redirect("https://softuni.bg");

        public HttpResponse LocalRedirect()
            => Redirect("/Dogs");

        public HttpResponse Error()
            => throw new InvalidOperationException("Invalid Action");
    }
}
