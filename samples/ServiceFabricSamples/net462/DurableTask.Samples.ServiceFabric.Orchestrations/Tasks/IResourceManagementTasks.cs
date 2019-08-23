using DurableTask.Samples.ServiceFabric.Orchestrations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurableTask.Samples.ServiceFabric.Orchestrations.Tasks
{
	public interface IResourceManagementTasks
	{
		Task<ResourceConfig> Create(string region);
		Task<bool> Delete(ResourceConfig resource);
		Task<ResourceConfig> Update(ResourceConfig resource, string version);
	}
}
