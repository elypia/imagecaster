using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ImageCasterApi
{
    public class ImageCasterApi
    {
        public static void Main(string[] args)
        {
            IHostBuilder builder = Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
                
            builder.Build().Run();
        }
    }
}
