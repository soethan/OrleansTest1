using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using OrleansTest1.GrainInterfaces1;
using Orleans.Runtime.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OrleansTest1.WebApi1.Controllers
{
	[Route("api/hello")]
    public class HelloController : Controller
    {
		[HttpGet]
		public IEnumerable<string> Get()
		{
			var helloGrain = GrainClient.GrainFactory.GetGrain<IGrain1>(0);
			return new string[] { helloGrain.SayHello().Result };
		}
	}
}