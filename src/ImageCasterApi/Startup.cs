using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ImageCasterApi
{
    public class Startup
    {
        /// <summary>All HTTP methods to allow to this application.</summary>
        public static readonly string[] Methods =
        {
            HttpMethod.Get.ToString(),
            HttpMethod.Post.ToString()
        };
        
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors((options) =>
            {
                options.AddPolicy("CORS", (builder) =>
                {
                    builder.WithOrigins(
                        "http://localhost:3000",
                        "http://127.0.0.1:3000"
                    ).AllowAnyHeader().WithMethods(Methods);
                });
            }).AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting().UseCors().UseEndpoints((endpoints) =>
            {
                endpoints.MapControllers();
            });
        }
    }
}