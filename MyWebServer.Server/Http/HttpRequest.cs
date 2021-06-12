using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MyWebServer.Server.Http
{
    public class HttpRequest
    {
        private const string NewLine = "\r\n";

        public HttpMethod Method { get; private set; }

        public string Path { get; private set; }

        public IReadOnlyDictionary<string , string > Query { get; private set; }
        public IReadOnlyDictionary<string, string> Form { get; private set; }

        public IReadOnlyDictionary<string, HttpHeader> Headers { get; private set; }

        public IReadOnlyDictionary<string, HttpCookie> Cookies { get; private set; }


        public string Body  { get;private set; }

        public static HttpRequest Parse(string request)
        {
            var lines = request.Split(NewLine);


            var startLine = lines.First().Split(" ");

            var method = ParseHttpMethod(startLine[0]);
            var url = startLine[1];

            var (path, query) = ParseUrl(url);

            var headers = parseHttpHeaderCollection(lines.Skip(1));

            var cookies = ParseHttpCookies(headers);


            var bodyLines = lines.Skip(headers.Count + 2).ToArray();
            var body = string.Join(NewLine,bodyLines);

            var form = ParseForm(headers, body);

            return new HttpRequest()
            {
                Method = method,
                Path = path,
                Query = query,
                Headers = headers,
                Cookies = cookies,
                Body = body,
                Form = form,
            };
        }

        private static Dictionary<string , HttpCookie> ParseHttpCookies(Dictionary<string, HttpHeader> headers)
        {
            var cookieCollection = new Dictionary<string, HttpCookie>();

            if (headers.ContainsKey(HttpHeader.Cookie))
            {
                var cookieHeader = headers[HttpHeader.Cookie];

                cookieHeader
                    .Value
                    .Split(';')
                    .Select(c => c.Split('='))
                    .Select(cp => new
                    {
                        Name = cp[0].Trim(),
                        Value = cp[1].Trim(),
                    }).ToList()
                    .ForEach(c=>cookieCollection.Add(c.Name , new HttpCookie(c.Name , c.Value)));
            }


            return cookieCollection;
        }


        private static Dictionary<string, HttpHeader> parseHttpHeaderCollection(IEnumerable<string> headerLines)
        {
            var headerCollection = new Dictionary<string , HttpHeader>();

            foreach (var headerLine in headerLines)
            {
                if (headerLine == string.Empty)
                {
                    break;
                }

                var headerParts = headerLine.Split(":", 2);

                if (headerParts.Length != 2)
                {
                    throw new InvalidOperationException("Request is not valid!");   
                }

                var headerName = headerParts[0];
                var headerValue = headerParts[1];


                var header = new HttpHeader(headerName, headerValue);
            
                headerCollection.Add(headerName, header);
            }

            return headerCollection;
        }

        private static HttpMethod ParseHttpMethod(string method)
        {
            return method.ToUpper() switch
            {
                "GET" => HttpMethod.Get,
                "POST" => HttpMethod.Post,
                "PUT" => HttpMethod.Put,
                "DELETE" => HttpMethod.Delete,
                _ => throw new InvalidOperationException($"Method {method} is not supported!"),
            };
        }

        private static (string , Dictionary<string , string>) ParseUrl(string url)
        {
            var urlParts = url.Split('?' , 2 );

            var path = urlParts[0].ToLower();
            var query = urlParts.Length > 1 ? ParseQuery(urlParts[1]) : new Dictionary<string, string>();

            return (path, query);
        }

        private static Dictionary<string, string> ParseQuery(string queryString)
        {
                return queryString
                    .Split('&')
                    .Select(part => part.Split('='))
                    .Where(part=>part.Length==2)
                    .ToDictionary(part => part[0] , part => part[1]);
        }
        private static Dictionary<string, string> ParseForm(Dictionary<string, HttpHeader> headers, string body)
        {
            var result = new Dictionary<string, string>();

            if (headers.ContainsKey(HttpHeader.ContentType)
                && headers[HttpHeader.ContentType].Value == HttpContentType.FormUrlEncoded)
            {
                result = ParseQuery(body);
            }

            return result;
        }
    }
}
