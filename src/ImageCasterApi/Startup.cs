using System.Collections.Generic;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;
using ImageCasterApi.Json.Converters;
using ImageCasterCore.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ImageCasterApi
{
    public class Startup
    {
        /// <summary>The name of the CORS configuration to use at runtime.</summary>
        public const string CorsProfile = "CORS";

        /// <summary>The Content-Type we Consume and Produce.</summary>
        public const string ContentType = MediaTypeNames.Application.Json;
        
        /// <summary>Origins allowed to make requests.</summary>
        public static readonly string[] Origins =
        {
            "http://0.0.0.0:80", "https://0.0.0.0:443",
            "http://localhost:3000", "http://127.0.0.1:3000",
            "http://localhost:5500", "http://127.0.0.1:5500"
        };
        
        /// <summary>All HTTP methods to allow to this application.</summary>
        public static readonly string[] Methods =
        {
            "GET", "POST"
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
                options.AddPolicy(CorsProfile, (builder) =>
                {
                    builder.WithOrigins(Origins).WithMethods(Methods).AllowAnyHeader();
                });
            });

            services.AddMvc((options) =>
            {
                FilterCollection filters = options.Filters;
                filters.Add(new ProducesAttribute(ContentType));
                filters.Add(new ConsumesAttribute(ContentType));
            });

            services.AddControllersWithViews().AddJsonOptions((options) =>
            {
                JsonSerializerOptions serializerOptions = options.JsonSerializerOptions;
                serializerOptions.AllowTrailingCommas = false;
                serializerOptions.WriteIndented = false;
                serializerOptions.IgnoreNullValues = true;
                
                // Add ImageCaster converters that we need in general.
                serializerOptions.AddImageCasterConverters();
                
                // Add converters that we need on the API specifically.
                IList<JsonConverter> converters = serializerOptions.Converters;
                converters.Add(new FrontendFileConverter());
            });
            
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting().UseCors(CorsProfile);
            
            app.UseEndpoints((endpoints) =>
            {
                endpoints.MapControllers();
            });
        }
    }
}