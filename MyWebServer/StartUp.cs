﻿using System.Threading.Tasks;
using MyWebServer.Controllers;
using MyWebServer.Server;
using MyWebServer.Server.Results;
using MyWebServer.Server.Controllers;

namespace MyWebServer
{
    public class StartUp
    {
        public static async Task Main(string[] args)
        {
            var server = new HttpServer(routingTable => routingTable
                .MapGet<HomeController>("/", c => c.Index())
                .MapGet<HomeController>("/ToDogs", c => c.LocalRedirect())
                .MapGet<AnimalsController>("/Cats", c => c.Cats())
                .MapGet<AnimalsController>("/Dogs", c => c.Dogs())
                .MapGet<AnimalsController>("/Turtles", c => c.Turtles())
                .MapGet<HomeController>("/softuni" , c=>c.toSoftUni())
                .MapGet<AccountController>("/Cookies", c => c.ActionWithCookies())

                .MapGet<CatsController>("/Cats/Create" , c=>c.Create())
                .MapPost<CatsController>("/Cats/Save" , c=>c.Save()));

                await server.Start();
        }
    }
}
