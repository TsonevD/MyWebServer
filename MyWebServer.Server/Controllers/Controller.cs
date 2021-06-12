using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using MyWebServer.Server.Http;
using MyWebServer.Server.Responses;

namespace MyWebServer.Server.Controllers
{
    public abstract class Controller
    {
        protected Controller(HttpRequest request)
        {
            this.Request = request;
        }

        protected HttpRequest Request { get; private set; }

        protected HttpResponse Text(string text)
        {
            return new TextResponse(text);
        }

        protected HttpResponse Html(string html)
        {
            return new HtmlResponse(html);
        }


        protected HttpResponse Redirect(string location)
        {
            return new RedirectResponse(location);
        }

        protected HttpResponse View([CallerMemberName] string viewName ="")
        {
            return new ViewResponse(viewName, this.GetControllerName());
        }

        private string GetControllerName() => this.GetType().Name.Replace(nameof(Controller), string.Empty);


    }
}
