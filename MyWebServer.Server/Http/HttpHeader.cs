using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using MyWebServer.Server.Common;

namespace MyWebServer.Server.Http
{
    public class HttpHeader
    {
        public const string ContentType = "Content-Type";
        public const string ContentLength = "Content-Length";
        public const string Server = "Server";
        public const string Date = "Date";
        public const string Location = "Location";
        public const string SetCookie = "Set-Cookie";
        public const string Cookie = "Cookie";




        public HttpHeader(string name , string value)
        {
            Guard.AgainstNull(name, nameof(name));
            Guard.AgainstNull(value, nameof(value));

            this.Name = name;
            this.Value = value;
        }
        public string Name { get; init; }

        public string Value { get; init; }
        public override string ToString()
        {
            
            return $"{this.Name}: {this.Value}";
        }
    }

}
