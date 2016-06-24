using System.Web.Http;
using Owin;

namespace KP.WebApi.Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            ServerConfig.ConfigureRoutes(config);
            ServerConfig.ConfigureFormatters(config);
            app.UseStaticFiles();
            app.UseWebApi(config);
            
        }
    }
}