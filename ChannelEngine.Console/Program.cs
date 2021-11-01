using ChannelEngine.Common.Client;
using ChannelEngine.Common.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace ChannelEngine.Console
{
	public static class Program
    {
        public static void Main(string[] args)
        {
            new HostBuilder()
               .ConfigureHostConfiguration(configHost =>
               {
                   configHost.SetBasePath(Directory.GetCurrentDirectory());
                   configHost.AddEnvironmentVariables(prefix: "ASPNETCORE_");
                   configHost.AddCommandLine(args);
               })
               .ConfigureAppConfiguration((hostContext, configApp) =>
               {
                   configApp.AddJsonFile("appsettings.json", optional: true);
               })
               .ConfigureServices((hostContext, services) =>
               {
                   var config = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json", optional: true)
                     .Build();
                   services.AddHostedService<ChannelEngineHostedService>();
                   services.AddTransient<IUrlProvider, UrlProvider>();
                   services.AddHttpClient<IChannelEngineClient, ChannelEngineClient>();

                   services.AddTransient<ChannelEngineConfiguration>();
               })
			   .UseConsoleLifetime()
               .Build()
               .Run();
        }
    }
}
