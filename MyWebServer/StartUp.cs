using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MyWebServer.Controllers;
using MyWebServer.Server;
using MyWebServer.Server.Responses;
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
                .MapGet<HomeController>("/softuni" , c=>c.toSoftUni()));

            await server.Start();
        }
    }
}
