using KP.WebApi.Server;
using Topshelf;

namespace KP.WebApi
{
    public static class Program
    {
        private static void Main()
        {
            HostFactory.Run(x =>
            {
                x.Service<ServiceHost>(s =>
                {
                    s.ConstructUsing(name => new ServiceHost());
                    s.WhenStarted(svc => svc.Start());
                    s.WhenStopped(svc => svc.Stop());
                });
                x.RunAsLocalSystem();
                x.SetServiceName("KPWebApi");

            });

        }
    }
}