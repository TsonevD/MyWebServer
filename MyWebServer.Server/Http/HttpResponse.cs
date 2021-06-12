using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebServer.Server.Common;

namespace MyWebServer.Server.Http
{
    public abstract class HttpResponse
    {
        protected HttpResponse(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;

            this.Headers.Add(HttpHeader.Server ,new HttpHeader(HttpHeader.Server, "My Web Server"));
            this.Headers.Add(HttpHeader.Date, new HttpHeader(HttpHeader.Date , $"{DateTime.UtcNow:r}"));
        }
        public HttpStatusCode StatusCode { get; protected set; }

        public IDictionary<string , HttpHeader> Headers { get; } = new Dictionary<string, HttpHeader>();

        public string Content { get; protected set; }

        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine($"HTTP/1.1 {(int) this.StatusCode} {this.StatusCode}");

            foreach (var header in this.Headers.Values)
            {
                result.AppendLine($"{header.ToString()}");
            }

            if (!string.IsNullOrEmpty(this.Content))
            {
                result.AppendLine();
                //Could be AppendLine
                result.Append(this.Content);
            }
    
            return result.ToString();
        }

        protected void PrepareContent(string content, string contentType)
        {
            Guard.AgainstNull(content, nameof(content));
            Guard.AgainstNull(contentType, nameof(contentType));

            var contentLength = Encoding.UTF8.GetBytes(content).Length;

            this.Headers.Add(HttpHeader.ContentType, new HttpHeader(HttpHeader.ContentType , contentType));
            this.Headers.Add(HttpHeader.ContentLength, new HttpHeader(HttpHeader.ContentLength , contentLength.ToString()));

            this.Content = content;
        }
    }
}
