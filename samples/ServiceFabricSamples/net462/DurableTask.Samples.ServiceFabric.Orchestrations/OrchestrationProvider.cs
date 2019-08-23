using DurableTask.Core;
using DurableTask.Samples.ServiceFabric.Orchestrations.Orchestration;
using DurableTask.Samples.ServiceFabric.Orchestrations.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurableTask.Samples.ServiceFabric.Orchestrations
{
	public class OrchestrationProvider
	{
		private IServiceProvider serviceProvider;

		public OrchestrationProvider(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		public void RegisterOrchestrations(TaskHubWorker taskHubWorker)
		{
			//Add orchestrations
			taskHubWorker.AddTaskOrchestrations(typeof(ManageResourceOrchestration));

			//Add activities
			taskHubWorker.AddTaskActivitiesFromInterface(serviceProvider.GetRequiredService<ILoggerTasks>(), true);
			taskHubWorker.AddTaskActivitiesFromInterface(serviceProvider.GetRequiredService<IResourceManagementTasks>(), true);
		}
	}
}
