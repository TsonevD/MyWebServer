using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MyWebServer.Controllers;
using MyWebServer.Server;
using MyWebServer.Server.Responses;

namespace MyWebServer
{
   public class StartUp
    {
        public static async Task Main(string[] args)
        {
            var server = new HttpServer(routingTable => routingTable
                .MapGet("/" , new TextResponse("Hello from Dimitar!"))
                .MapGet("/Cats" ,request =>
                {
                    const string nameKey = "Name";
                    var query = request.Query;

                    var catName = query.ContainsKey(nameKey) ? query[nameKey] : "the cats";

                    var result = $"<h1>Hello from {catName}!!!</h1>";

                    return new HtmlResponse(result);
                })
                .MapGet("/Dogs" , new TextResponse("Marcho Rullzzz!!!")));

            await server.Start();
        }
    }
}
