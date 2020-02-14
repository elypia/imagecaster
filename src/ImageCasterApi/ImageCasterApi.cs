using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace ImageCasterApi
{
    public class ImageCasterApi
    {
        public static void Main(string[] args)
        {
            IHostBuilder builder = Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            }).ConfigureLogging((logging) =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.Trace);
            }).UseNLog();

            builder.Build().Run();
        }
    }
}
