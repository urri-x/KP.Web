using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Http.SelfHost;

namespace KP.WebApi.Server
{
    static internal class ServerConfig
    {
        public static void ConfigureRoutes(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute(
                name: "Home",
                routeTemplate: "",
                defaults: new { controller = "Home", action = "Get" }
                );

            configuration.Routes.MapHttpRoute(name: "DefaultViewRoute",
                routeTemplate: "{controller}/{action}");

            configuration.Routes.MapHttpRoute(name: "DefaultViewRouteWithParam",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new {id = 0}
                );
            
        }
        public static void ConfigureFormatters(HttpConfiguration configuration)
        {
            configuration.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }

    }
}