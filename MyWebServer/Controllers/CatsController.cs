using MyWebServer.Server.Controllers;
using MyWebServer.Server.Http;

namespace MyWebServer.Controllers
{
    public class CatsController : Controller
    {
        public CatsController(HttpRequest request)
            : base(request)
        {
        }

        public HttpResponse Create() => View();

        public HttpResponse Save()
        {
            var name = this.Request.Form["Name"];
            var age = this.Request.Form["Age"];

            return Text($"My name is {name}, {age} years old!");
        }


    }
}
