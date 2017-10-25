using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Orleans.Runtime.Configuration;
using Orleans;
using Orleans.Runtime;

namespace OrleansTest1.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

			//var config = ClientConfiguration.LocalhostSilo(30000);

			//// Attempt to connect a few times to overcome transient failures and to give the silo enough 
			//// time to start up when starting at the same time as the client (useful when deploying or during development).
			//const int initializeAttemptsBeforeFailing = 5;

			//int attempt = 0;
			//while (true)
			//{
			//	try
			//	{
			//		GrainClient.Initialize(config);
			//		break;
			//	}
			//	catch (SiloUnavailableException e)
			//	{
			//		attempt++;
			//		if (attempt >= initializeAttemptsBeforeFailing)
			//		{
			//			throw;
			//		}
			//		System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
			//	}
			//}
		}
    }
}
