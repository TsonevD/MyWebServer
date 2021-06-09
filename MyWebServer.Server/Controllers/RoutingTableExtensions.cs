using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebServer.Server.Http;
using MyWebServer.Server.Routing;

namespace MyWebServer.Server.Controllers
{
    public static class RoutingTableExtensions
    {
        public static IRoutingTable MapGet<TController>(
            this IRoutingTable routingTable, 
        string path, 
            Func<TController, HttpResponse> controllerFunction)
            where TController : Controller 
        => routingTable.MapGet(path , request =>
        {
            return controllerFunction(CreateController<TController>(request));
        });

        public static IRoutingTable MapPost<TController>(
            this IRoutingTable routingTable,
            string path,
            Func<TController, HttpResponse> controllerFunction)
            where TController : Controller
            => routingTable.MapGet(path, request =>
            {
                return controllerFunction(CreateController<TController>(request));
            });

        private static TController CreateController<TController>(HttpRequest request)
        {
            return (TController)Activator.CreateInstance(typeof(TController), new[] {request});
        }
    }
}
