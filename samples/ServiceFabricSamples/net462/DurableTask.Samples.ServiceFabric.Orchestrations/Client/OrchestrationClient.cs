using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DurableTask.Core;

namespace DurableTask.Samples.ServiceFabric.Orchestrations.Client
{
	public class OrchestrationClient : IOrchestrationClient
	{
		private readonly TaskHubClient taskHubClient;

		public OrchestrationClient(TaskHubClient taskHubClient)
		{
			this.taskHubClient = taskHubClient;
		}

		public async Task<OrchestrationInstance> CreateOrchestrationAsync<TInput>(Type orchestrationType, string orchestrationInstanceId, TInput input, CancellationToken cancellationToken)
		{
			OrchestrationStatus[] dedupeStatuses = { OrchestrationStatus.Running, OrchestrationStatus.Pending };

			return await taskHubClient.CreateOrchestrationInstanceAsync(orchestrationType, orchestrationInstanceId, input, dedupeStatuses);
		}
	}
}
