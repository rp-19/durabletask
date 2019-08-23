using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DurableTask.Samples.ServiceFabric.Orchestrations.Models;

namespace DurableTask.Samples.ServiceFabric.Orchestrations.Tasks
{
	public class ResourceManagementTasks : IResourceManagementTasks
	{
		public async Task<ResourceConfig> Create(string region)
		{
			await Task.Delay(1);
			Console.WriteLine("Resource created");
			return new ResourceConfig
			{
				Name = "test",
				Region = region,
				Version = "1",
				CreatedOn = DateTime.UtcNow
			};
		}

		public Task<bool> Delete(ResourceConfig resource)
		{
			Console.WriteLine("Resource Deleted");
			return Task.FromResult(true);
		}

		public Task<ResourceConfig> Update(ResourceConfig resource, string version)
		{
			Console.WriteLine("Resource updated");
			resource.Version = version;
			return Task.FromResult(resource);
		}
	}
}
