﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MyWebServer.Server.Http;

namespace MyWebServer.Server.Routing
{
    public interface IRoutingTable
    {
        IRoutingTable Map(HttpMethod method , string path, HttpResponse response);

        IRoutingTable Map(HttpMethod method, string path, Func<HttpRequest,HttpResponse> responseFunction);

        IRoutingTable MapGet(string path, HttpResponse response);

        IRoutingTable MapGet(string path, Func<HttpRequest , HttpResponse> responseFunction);


        IRoutingTable MapPost(string path, HttpResponse response);

        IRoutingTable MapPost(string path, Func<HttpRequest, HttpResponse> responseFunction);


    }
}