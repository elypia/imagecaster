using System.Collections.Generic;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;
using ImageCasterApi.Json.Converters;
using ImageCasterCore.Json.Converters;
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

        /// <summary>Origins allowed to make requests.</summary>
        public static readonly string[] Origins =
        {
            "http://0.0.0.0:80", "https://0.0.0.0:443",
            "http://localhost:3000", "http://127.0.0.1:3000"
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
                filters.Add(new ProducesAttribute(MediaTypeNames.Application.Json));
                filters.Add(new ConsumesAttribute(MediaTypeNames.Application.Json));
            });

            services.AddControllersWithViews().AddJsonOptions((options) =>
            {
                JsonSerializerOptions serializerOptions = options.JsonSerializerOptions;
                serializerOptions.AllowTrailingCommas = false;
                serializerOptions.WriteIndented = false;
                serializerOptions.IgnoreNullValues = true;

                IList<JsonConverter> converters = serializerOptions.Converters;
                
                // Add ImageCaster converters that we need in general.
                converters.Add(new FilterTypeConverter());
                converters.Add(new PercentageConverter());
                
                // Add converters that we need on the API specifically.
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