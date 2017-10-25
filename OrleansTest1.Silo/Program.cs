using System;

using Orleans;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;
using OrleansTest1.GrainInterfaces1;

namespace OrleansTest1.Silo
{
	/// <summary>
	/// Orleans test silo host
	/// </summary>
	public class Program
	{
		static void Main(string[] args)
		{
			//First, configure and start a local silo
			//var siloConfig = ClusterConfiguration.LocalhostPrimarySilo(30000);
			//var silo = new SiloHost("TestSilo", siloConfig);
			//silo.InitializeOrleansSilo();
			//silo.StartOrleansSilo();

			//Console.WriteLine("Silo started.");

			Console.WriteLine("Waiting for Orleans Silo to start. Press Enter to proceed...");
			Console.ReadLine();

			// Then configure and connect a client.
			var clientConfig = ClientConfiguration.LocalhostSilo(30000);
			var client = new ClientBuilder().UseConfiguration(clientConfig).Build();
			client.Connect().Wait();

			Console.WriteLine("Client connected.");

			var helloGrain = client.GetGrain<OrleansTest1.GrainInterfaces1.IGrain1>(0);
			//var helloGrain = GrainClient.GrainFactory.GetGrain<IGrain1>(0);
			Console.WriteLine("\n\n{0}\n\n", helloGrain.SayHello().Result);

			Console.WriteLine("\nPress Enter to terminate...");
			Console.ReadLine();

			// Shut down
			client.Close();
			//silo.ShutdownOrleansSilo();
		}
	}
}
