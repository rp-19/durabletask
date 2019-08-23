using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DurableTask.Core;
using DurableTask.Samples.ServiceFabric.Orchestrations.Models;
using DurableTask.Samples.ServiceFabric.Orchestrations.Tasks;

namespace DurableTask.Samples.ServiceFabric.Orchestrations.Orchestration
{
	public class ManageResourceOrchestration : BaseOrchestration<bool, ManageResourceOrchestrationInput>
	{
		IResourceManagementTasks resourceManagementTasks;
		protected override void Initialize(OrchestrationContext context)
		{
			resourceManagementTasks = context.CreateClient<IResourceManagementTasks>(true);
		}

		protected override async Task<bool> ExecuteOrchestration(OrchestrationContext context, ManageResourceOrchestrationInput input)
		{
			try
			{
				var resource = await resourceManagementTasks.Create(input.Region);

				resource = await resourceManagementTasks.Update(resource, "2");

				await resourceManagementTasks.Delete(resource);
				return true;
			}
			catch (Exception ex)
			{
				await Logger.LogException(ex);
				throw;
			}
		}
	}

	public class ManageResourceOrchestrationInput : OrchestrationInput
	{
		public string Region { get; set; }
	}
}
