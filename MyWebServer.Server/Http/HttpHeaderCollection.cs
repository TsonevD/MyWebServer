﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server.Http
{
   public class HttpHeaderCollection : IEnumerable<HttpHeader>
   {
       private readonly Dictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, HttpHeader>();
        }

        public void Add(string name , string value)
        {
            var header = new HttpHeader(name, value);

            this.headers.Add(name, header);
        }

        public int Count => this.headers.Count;

        public IEnumerator<HttpHeader> GetEnumerator()
        {
            return this.headers.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
   }
}
