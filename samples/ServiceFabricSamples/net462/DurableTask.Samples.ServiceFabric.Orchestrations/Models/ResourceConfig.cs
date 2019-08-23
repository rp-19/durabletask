using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurableTask.Samples.ServiceFabric.Orchestrations.Models
{
	public class ResourceConfig
	{
		public string Name { get; set; }
		public string Region { get; set; }
		public string Version { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime ModifiedOn { get; set; }
	}
}
