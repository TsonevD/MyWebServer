using System.Text;
using MyWebServer.Server.Common;
using MyWebServer.Server.Http;

namespace MyWebServer.Server.Responses
{
    public class ContentResponse : HttpResponse
    {
        public ContentResponse(string content, string contentType)
            : base(HttpStatusCode.OK)
        {
            Guard.AgainstNull(content , nameof(content));
            Guard.AgainstNull(contentType , nameof(content));

            this.PrepareContent(content, contentType);
        }
    }
}
