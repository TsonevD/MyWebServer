using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebServer.Server.Common;
using MyWebServer.Server.Http;

namespace MyWebServer.Server.Responses
{
    public class ContentResponse : HttpResponse
    {
        public ContentResponse(string text, string contentType)
            : base(HttpStatusCode.OK)
        {
            Guard.AgainstNull(text);
            ;

            var contentLength = Encoding.UTF8.GetBytes(text).Length;

            this.Headers.Add("Content-Type", contentType);
            this.Headers.Add("Content-Length", contentLength.ToString());

            this.Content = text;
        }
    }
}
