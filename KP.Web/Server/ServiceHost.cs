using System;
using System.Web.Http.SelfHost;
using JetBrains.Annotations;
using Microsoft.Owin.Hosting;

namespace KP.WebApi.Server
{
    public class ServiceHost
    {
        private IDisposable server;
        private int port = 9999;

        public ServiceHost()
        {
        }
        public void Shutdown()
        {

        }

        public void Start()
        {
            StartOptions options = new StartOptions();
            options.Urls.Add($"http://localhost:{port}");
            options.Urls.Add($"http://127.0.0.1:{port}");
            options.Urls.Add($"http://{Environment.MachineName}:{port}");

            server = WebApp.Start<Startup>(options);
        }

        public void Stop()
        {
            server.Dispose();
        }
    }
}