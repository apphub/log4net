using System;

namespace AppHub.Log4net.Models
{
	internal class Session
	{
		public Guid? Id { get; set; }
		public string Project { get; set; }
		public string ProjectVersion { get; set; }
		public string SdkVersion { get; set; }
		public Guid SdkId { get; set; }
		public string Server { get; set; }
		public string UserAgent { get; set; }
		public Exception Exception { get; set; }
	}
}
