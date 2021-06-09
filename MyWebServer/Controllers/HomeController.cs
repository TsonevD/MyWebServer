﻿using MyWebServer.Server.Controllers;
using MyWebServer.Server.Http;
using MyWebServer.Server.Responses;

namespace MyWebServer.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(HttpRequest request)
            : base(request)
        {
        }
        public HttpResponse Index()
        {
            return Text("Hello from Dimitar!");
        }
    }
}
