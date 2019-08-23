using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DurableTask.Samples.ServiceFabric.Orchestrations;
using DurableTask.Samples.ServiceFabric.Orchestrations.Client;
using DurableTask.Samples.ServiceFabric.Orchestrations.Orchestration;
using Microsoft.Extensions.DependencyInjection;

namespace DurableTasks.Samples.ServiceFabric.Client
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				IServiceCollection serviceCollection = new ServiceCollection();
				serviceCollection.AddOrchestrationClientModules();

				ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

				var orchestrationClient = serviceProvider.GetRequiredService<IOrchestrationClient>();
				var instanceId = Guid.NewGuid().ToString();
				var instance = orchestrationClient.CreateOrchestrationAsync(
					typeof(ManageResourceOrchestration),
					instanceId,
					new ManageResourceOrchestrationInput
					{
						Region = "eastus"
					},
					CancellationToken.None).GetAwaiter().GetResult();

				Console.WriteLine("Orchestration created with instance id: " + instanceId);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
			
		}
	}
}
