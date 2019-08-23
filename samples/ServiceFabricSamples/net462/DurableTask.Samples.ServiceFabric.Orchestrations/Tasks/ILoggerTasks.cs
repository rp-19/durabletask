using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurableTask.Samples.ServiceFabric.Orchestrations.Tasks
{
	public interface ILoggerTasks
	{
		Task LogInfo(string message);
		Task LogException(Exception ex);
	}
}
