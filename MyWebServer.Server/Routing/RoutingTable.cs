﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyWebServer.Server.Common;
using MyWebServer.Server.Http;
using MyWebServer.Server.Responses;

namespace MyWebServer.Server.Routing
{
    public class RoutingTable : IRoutingTable
    {
        private readonly Dictionary<HttpMethod, Dictionary<string, Func<HttpRequest , HttpResponse>>> routes;

        public RoutingTable()
        {
            this.routes = routes = new ()
            {
                [HttpMethod.Get] = new (),
                [HttpMethod.Post] = new (),
                [HttpMethod.Put] = new (),
                [HttpMethod.Delete] = new(),
            };
        }

        public IRoutingTable Map(HttpMethod method , string path, HttpResponse response)
        {
            Guard.AgainstNull(response, nameof(response));


            return this.Map(method,path , request=> response);
         
        }

        public IRoutingTable Map(HttpMethod method, string path, Func<HttpRequest, HttpResponse> responseFunction)
        {
            Guard.AgainstNull(path, nameof(path));
            Guard.AgainstNull(responseFunction, nameof(responseFunction));

            this.routes[method][path] = responseFunction;

            return this;

        }

        public IRoutingTable MapGet(string path, HttpResponse response)
        {
            return MapGet(path, request => response);
        }

        public IRoutingTable MapGet(string path, Func<HttpRequest, HttpResponse> responseFunction)
        {
            return Map(HttpMethod.Get, path, responseFunction);
        }

        public IRoutingTable MapPost(string path, HttpResponse response)
        {
            return MapPost(path, request =>response);
        }

        public IRoutingTable MapPost(string path, Func<HttpRequest, HttpResponse> responseFunction)
        {
            return Map(HttpMethod.Post, path, responseFunction);
        }

        public HttpResponse MatchRequest(HttpRequest request)
        {
            var requestMethod = request.Method;
            var requestUrl = request.Path;

            if (!this.routes.ContainsKey(requestMethod) || !this.routes[requestMethod].ContainsKey(requestUrl))
            {
                return new NotFoundResponse();
            }

            var responseFunction = this.routes[requestMethod][requestUrl];

            return responseFunction(request);
        }
    }
}
