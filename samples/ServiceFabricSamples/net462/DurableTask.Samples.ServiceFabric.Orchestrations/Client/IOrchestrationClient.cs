using DurableTask.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DurableTask.Samples.ServiceFabric.Orchestrations.Client
{
	public interface IOrchestrationClient
	{
		Task<OrchestrationInstance> CreateOrchestrationAsync<TInput>(Type orchestrationType, string orchestrationInstanceId, TInput input, CancellationToken cancellationToken);

	}
}
