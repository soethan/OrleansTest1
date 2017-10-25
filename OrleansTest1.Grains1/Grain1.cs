using System.Threading.Tasks;
using Orleans;
using OrleansTest1.GrainInterfaces1;

namespace OrleansTest1.Grains1
{
	/// <summary>
	/// Grain implementation class Grain1.
	/// </summary>
	public class Grain1 : Grain, IGrain1
	{
		public Task<string> SayHello()
		{
			return Task.FromResult("Hello World!");
		}
	}
}
