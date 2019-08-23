using System;
using System.Diagnostics;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using DurableTask.Samples.ServiceFabric.Orchestrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Runtime;

namespace DurableTask.Samples.ServiceFabric.Worker
{
	internal static class Program
	{
		/// <summary>
		/// This is the entry point of the service host process.
		/// </summary>
		private static void Main()
		{
			try
			{
				// The ServiceManifest.XML file defines one or more service type names.
				// Registering a service maps a service type name to a .NET type.
				// When Service Fabric creates an instance of this service type,
				// an instance of the class is created in this host process.

				//Create new Service Collection
				IServiceCollection serviceCollection = new ServiceCollection();
				serviceCollection.AddOrchestrationServerModules();

				ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

				ServiceRuntime.RegisterServiceAsync("DurableTask.Samples.ServiceFabric.WorkerType",
					context => new Worker(context, serviceProvider)).GetAwaiter().GetResult();

				ServiceEventSource.Current.ServiceTypeRegistered(Process.GetCurrentProcess().Id, typeof(Worker).Name);

				// Prevents this host process from terminating so services keep running.
				Thread.Sleep(Timeout.Infinite);
			}
			catch (Exception e)
			{
				ServiceEventSource.Current.ServiceHostInitializationFailed(e.ToString());
				throw;
			}
		}
	}
}
