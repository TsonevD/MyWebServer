using System.Text;
using MyWebServer.Server.Common;
using MyWebServer.Server.Http;

namespace MyWebServer.Server.Results
{
    public class ContentResult : ActionResult
    {
        public ContentResult(HttpResponse response,string content, string contentType)
            : base(response)
        {
            Guard.AgainstNull(content , nameof(content));
            Guard.AgainstNull(contentType , nameof(content));

            this.PrepareContent(content, contentType);
        }
    }
}
