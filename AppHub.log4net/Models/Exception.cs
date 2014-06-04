using System;

namespace AppHub.Log4net.Models
{
	internal class Exception
	{
		public int? Id { get; set; }
		public string Orientation { get; set; }
		public string Message { get; set; }
		public string StackTrace { get; set; }
		public string Data { get; set; }
		public DateTime? CreatedOn { get; set; }
		public DateTime? ModifiedOn { get; set; }
	}
}
