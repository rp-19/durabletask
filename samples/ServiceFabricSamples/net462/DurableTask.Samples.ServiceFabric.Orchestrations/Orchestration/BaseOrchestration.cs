using DurableTask.Core;
using DurableTask.Samples.ServiceFabric.Orchestrations.Models;
using DurableTask.Samples.ServiceFabric.Orchestrations.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurableTask.Samples.ServiceFabric.Orchestrations.Orchestration
{
	public abstract class BaseOrchestration<TResult, TInput> : TaskOrchestration<TResult, TInput> where TInput : OrchestrationInput
	{
		protected ILoggerTasks Logger;

		protected abstract void Initialize(OrchestrationContext context);
		protected abstract Task<TResult> ExecuteOrchestration(OrchestrationContext context, TInput input);

		public override async Task<TResult> RunTask(OrchestrationContext context, TInput input)
		{
			try
			{
				Logger = context.CreateClient<ILoggerTasks>(true);
				Initialize(context);

				await Logger.LogInfo("started");
				var result = await ExecuteOrchestration(context, input);
				await Logger.LogInfo("completed");
				return result;
			}
			catch (Exception ex)
			{
				await Logger.LogException(ex);
				throw;
			}
		}
	}
}
