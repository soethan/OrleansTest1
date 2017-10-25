using System.Threading.Tasks;
using Orleans;

namespace OrleansTest1.GrainInterfaces1
{
	/// <summary>
	/// Grain interface IGrain1
	/// </summary>
	public interface IGrain1 : IGrainWithIntegerKey
	{
		Task<string> SayHello();
	}
}
