using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server.Http
{
    public enum HttpStatusCode
    {
        OK = 200,
        Found = 302,
        BadRequest = 400,
        NotFound = 404,
        InternalServerError=500,
    }
}
