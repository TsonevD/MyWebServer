
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MyWebServer.Server.Common;

namespace MyWebServer.Server.Http
{
    public class HttpSession
    {
        public const string SessionCookieName = "MyWebServerSID";

        private Dictionary<string, string> data;

        public HttpSession(string id)
        {
            Guard.AgainstNull(id, nameof(id));
            this.Id = id;

            this.data = new Dictionary<string, string>();
        }

        public string this[string key]
        {
            get => this.data[key];
            set => this.data[key] = value;
        }
        public string Id { get; init; }

        public int Count
            => this.data.Count;

        public bool ContainsKey(string key) 
            => this.data.ContainsKey(key);

       
    }
}
