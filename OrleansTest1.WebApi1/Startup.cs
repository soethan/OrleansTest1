using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans.Runtime;
using Orleans;
using Orleans.Runtime.Configuration;

namespace OrleansTest1.WebApi1
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

			var config = ClientConfiguration.LoadFromFile("ClientConfiguration.xml");
			//var config = ClientConfiguration.LocalhostSilo(30000);

			// Attempt to connect a few times to overcome transient failures and to give the silo enough 
			// time to start up when starting at the same time as the client (useful when deploying or during development).
			const int initializeAttemptsBeforeFailing = 5;

			int attempt = 0;
			while (true)
			{
				try
				{
					GrainClient.Initialize(config);
					break;
				}
				catch (SiloUnavailableException e)
				{
					attempt++;
					if (attempt >= initializeAttemptsBeforeFailing)
					{
						throw;
					}
					System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
				}
			}
		}
	}
}
