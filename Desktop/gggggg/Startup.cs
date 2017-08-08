using System.Web.Http;
using System.Web.Http.SelfHost;

namespace DXzonghejiaofei
{
    public class Startup
    {
        public static void Configuration(string url)
        {
            var config = new HttpSelfHostConfiguration(url);
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            var server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();
        }
    }
}