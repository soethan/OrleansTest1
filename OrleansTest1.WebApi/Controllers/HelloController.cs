using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using OrleansTest1.GrainInterfaces1;
using Orleans.Runtime.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OrleansTest1.WebApi.Controllers
{
	[Route("api/hello")]
    public class HelloController : Controller
    {
		[HttpGet]
		public IEnumerable<string> Get()
		{
			//var clientConfig = ClientConfiguration.LoadFromFile("ClientConfiguration.xml");
			//GrainClient.Initialize(clientConfig);
			//var helloGrain = GrainClient.GrainFactory.GetGrain<IGrain1>(0);

			var clientConfig = ClientConfiguration.LocalhostSilo(30000);
			var client = new ClientBuilder().UseConfiguration(clientConfig).Build();
			client.Connect().Wait();
			var helloGrain = client.GetGrain<IGrain1>(0);

			return new string[] { helloGrain.SayHello().Result };
		}
	}
}