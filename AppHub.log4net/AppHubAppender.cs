using System;
using System.IO;
using System.Linq;
using AppHub.Log4net.Models;
using log4net;
using log4net.Appender;
using log4net.Core;
using RestSharp;
using Exception = AppHub.Log4net.Models.Exception;

namespace AppHub.Log4net
{
    public class AppHubAppender : AppenderSkeleton
	{
		private string Host { get; set; }
		private Guid SdkId { get; set; }
		private string Version { get; set; }
		private string Project { get; set; }

	    protected override void Append(LoggingEvent loggingEvent)
	    {
		    try
		    {
			    var pattern = new StringWriter();
				base.Layout.Format(pattern, loggingEvent);
			    
				if (string.IsNullOrWhiteSpace(Version)) Version = "1";

				//Initialize connection to host
			    var client = new RestClient(Host);
			    var request = new RestRequest(string.Format("api/v{0}/error", Version), Method.POST)
			    {
				    RequestFormat = DataFormat.Json/*, 
					JsonSerializer = new RestSharpJsonNetSerializer()*/
			    };

			    //build models to send to AppHub
			    var exception = new Exception
			    {
				    Message = loggingEvent.MessageObject.ToString(),
				    Data = pattern.ToString(),
				    CreatedOn = loggingEvent.TimeStamp,
				    ModifiedOn = loggingEvent.TimeStamp
			    };
			    if (loggingEvent.ExceptionObject != null)
			    {
				    exception.StackTrace = loggingEvent.ExceptionObject.StackTrace;
			    }
			    var session = new Session
			    {
				    SdkId = SdkId,
					Project = Project,
					Exception = exception
			    };
			    if (ThreadContext.Properties != null)
			    {
				    var props = ThreadContext.Properties;
				    var keys = props.GetKeys();
				    if (keys.Contains("CurrentRequestUserAgent"))
					    session.UserAgent = props["CurrentRequestUserAgent"].ToString();
				    if (keys.Contains("CurrentRequestServerId"))
					    session.UserAgent = props["CurrentRequestServerId"].ToString();
			    }

				//Add model to body as JSON
			    request.AddBody(session);

				//Send to AppHub
			    var response = client.Execute(request);
		    }
		    catch (System.Exception e)
		    {
			    Console.WriteLine(e);
		    }
	    }
	}
}
