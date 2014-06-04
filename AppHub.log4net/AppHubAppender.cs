using log4net.Appender;
using log4net.Core;

namespace AppHub.Log4net
{
    public class AppHubAppender : AppenderSkeleton
	{
		private string Host { get; set; }

	    protected override void Append(LoggingEvent loggingEvent)
		{
			
		}
	}
}
