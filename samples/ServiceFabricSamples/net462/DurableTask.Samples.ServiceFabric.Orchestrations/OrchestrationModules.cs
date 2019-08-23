using DurableTask.Core;
using DurableTask.Samples.ServiceFabric.Orchestrations.Client;
using DurableTask.Samples.ServiceFabric.Orchestrations.Tasks;
using DurableTask.ServiceBus;
using DurableTask.ServiceBus.Tracking;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurableTask.Samples.ServiceFabric.Orchestrations
{
	public static class OrchestrationModules
	{
		public static IServiceCollection AddOrchestrationClientModules(this IServiceCollection services)
		{
			string taskHubName = "";
			string serviceBusConnectionString = "";
			string storageConnectionString = "";

			services.AddSingleton<IOrchestrationServiceInstanceStore>((serviceProvider) => {

				return new AzureTableInstanceStore(taskHubName, storageConnectionString);
			});

			services.AddSingleton((serviceProvider) =>
			{
				var instanceStore = serviceProvider.GetRequiredService<IOrchestrationServiceInstanceStore>();
				return new ServiceBusOrchestrationService(serviceBusConnectionString, taskHubName, instanceStore, null, null);
			});

			services.AddSingleton<IOrchestrationService>((serviceProvider) => serviceProvider.GetRequiredService<ServiceBusOrchestrationService>());
			services.AddSingleton<IOrchestrationServiceClient>((serviceProvider) => serviceProvider.GetRequiredService<ServiceBusOrchestrationService>());

			services.AddTransient((serviceProvider) =>
			{
				var serviceClient = serviceProvider.GetRequiredService<IOrchestrationServiceClient>();
				return new TaskHubClient(serviceClient);
			});

			services.AddTransient<IOrchestrationClient, OrchestrationClient>();

			return services;
		}

		public static IServiceCollection AddOrchestrationServerModules(this IServiceCollection services)
		{
			services.AddOrchestrationClientModules();

			services.AddSingleton<ILoggerTasks, LoggerTasks>();
			services.AddTransient<IResourceManagementTasks, ResourceManagementTasks>();

			return services;
		}
	}
}
