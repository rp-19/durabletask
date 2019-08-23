using DurableTask.Core;
using DurableTask.Samples.ServiceFabric.Orchestrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DurableTask.Samples.ServiceFabric.Worker
{
	public class DTFCommunicationListener : ICommunicationListener, IDisposable
	{
		private TaskHubWorker taskHubWorker;
		private IServiceProvider serviceProvider;

		public DTFCommunicationListener(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		public async Task<string> OpenAsync(CancellationToken cancellationToken)
		{
			var orchestrationService = serviceProvider.GetRequiredService<IOrchestrationService>();
			await orchestrationService.CreateIfNotExistsAsync();

			taskHubWorker = new TaskHubWorker(orchestrationService);
			OrchestrationProvider orchestrationProvider = new OrchestrationProvider(serviceProvider);
			orchestrationProvider.RegisterOrchestrations(taskHubWorker);

			await taskHubWorker.StartAsync();
			return "OrchestrationListener started";
		}

		public void Abort()
		{
			if (taskHubWorker != null)
			{
				taskHubWorker.StopAsync(true).GetAwaiter().GetResult();
			}

			this.Dispose();
		}

		public async Task CloseAsync(CancellationToken cancellationToken)
		{
			if (taskHubWorker != null)
			{
				await taskHubWorker.StopAsync();
			}

			this.Dispose();
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				taskHubWorker?.Dispose();
			}
		}
	}
}
