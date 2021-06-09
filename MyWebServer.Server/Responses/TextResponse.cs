using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebServer.Server.Common;
using MyWebServer.Server.Http;

namespace MyWebServer.Server.Responses
{
    public class TextResponse : ContentResponse
    {
        public TextResponse(string text)
            : base(text, "text/plain; charset=UTF-8")
        {
        }

    }
}
