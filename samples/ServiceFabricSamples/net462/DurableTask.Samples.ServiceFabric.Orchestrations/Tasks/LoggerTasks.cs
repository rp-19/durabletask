using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurableTask.Samples.ServiceFabric.Orchestrations.Tasks
{
	public class LoggerTasks : ILoggerTasks
	{
		public Task LogException(Exception ex)
		{
			Console.WriteLine(ex.Message);
			return Task.CompletedTask;
		}

		public Task LogInfo(string message)
		{
			Console.Write(message);
			return Task.CompletedTask;
		}
	}
}
