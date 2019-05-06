using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FleetManager
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            //Get current workfolder for dynamic relative paths
            var publishPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var builder = new ConfigurationBuilder()
                .SetBasePath(publishPath)
                .AddJsonFile(publishPath+"/appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }
        public static string ConnectionString {  
            get;  
            private set;  
        } 

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc()
            .AddJsonOptions(options => {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;});
            
<<<<<<< HEAD
            services.AddCors(options =>
            {
            //allow CORS (Cross origin resource request)
            options.AddDefaultPolicy(
                builder =>
                {
                   
                    /* builder.WithOrigins("http://example.com",
                                        "http://www.contoso.com"); */
                    builder.AllowAnyOrigin();
                });
            });
=======
            //Allow AJAX-calls from non-same origin trough CORS 
            //services.AddCors();
>>>>>>> 8fb916ef37585ca545b9e3132ff68e9a43c0b3c7
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            //use headers for Apache
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            //Allow CORS from one origin
            /* app.UseCors(builder =>
            builder.WithOrigins("http://very-trusted-site.com")
            .WithMethods("GET", "POST", "PUT", "DELETE")
            .AllowAnyHeader()); */

            //Allow CORS from any origin
            /* app.UseCors(builder =>
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()); */

            //get DB-data from appsettings.json
            ConnectionString = Configuration["ConnectionStrings:DefaultConnection"];
            app.UseMvc();
            app.UseCors();
        }
    }
}
