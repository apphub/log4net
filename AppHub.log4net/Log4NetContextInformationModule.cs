using System;
using System.Security.Principal;
using System.Web;

namespace AppHub.Log4net
{
	public class Log4NetContextInformationModule : IHttpModule
	{
		private HttpRequest _request;

		public void Init(HttpApplication app)
		{
			app.BeginRequest += (source, e) =>
				                    {
					                    var context = HttpContext.Current;
										_request = context.Request;
					                    global::log4net.ThreadContext.Properties["CurrentRequestUrl"] = CurrentRequestUrl(_request);
					                    global::log4net.ThreadContext.Properties["CurrentRequestUserAgent"] = CurrentRequestUserAgent(_request);
					                    global::log4net.ThreadContext.Properties["CurrentRequestUserAgentShort"] = CurrentRequestUserAgentShort(_request);
					                    global::log4net.ThreadContext.Properties["CurrentRequestReferrer"] = CurrentRequestReferrer(_request);
					                    global::log4net.ThreadContext.Properties["CurrentRequestServerId"] = CurrentRequestServerId(_request);
				                    };
			app.PostAuthenticateRequest += (source, e) =>
				                               {
												   var context = HttpContext.Current;
												   var user = context.User != null ? context.User.Identity : null;
												   global::log4net.ThreadContext.Properties["CurrentRequestUsername"] = CurrentRequestUsername(user);
											   };
			app.EndRequest += (source, e) =>
										{
											global::log4net.ThreadContext.Properties["CurrentRequestReferrer"] = CurrentRequestReferrer(_request);
										};
		}

		public void Dispose()
		{
			global::log4net.ThreadContext.Properties.Remove("CurrentRequestUrl");
			global::log4net.ThreadContext.Properties.Remove("CurrentRequestUsername");
			global::log4net.ThreadContext.Properties.Remove("CurrentRequestUserAgent");
			global::log4net.ThreadContext.Properties.Remove("CurrentRequestUserAgentShort");
			global::log4net.ThreadContext.Properties.Remove("CurrentRequestReferrer");
			global::log4net.ThreadContext.Properties.Remove("CurrentRequestServerId");
		}

		private static string CurrentRequestUrl(HttpRequest request)
		{
			if (request == null)
				return "No HttpContext or Request";

			Uri url = null;
			try
			{
				url = request.Url;
			}
			catch
			{
			}

			if (url == null)
				return string.Empty;

			return url.ToString();
		}
		private static string CurrentRequestUsername(IIdentity user)
		{
			if (user == null)
				return "No user identity";

			var username = string.Empty;
			try
			{
				username = user.IsAuthenticated ? user.Name : "(anonymous)";
			}
			catch
			{
			}
			return username;
		}
		private static string CurrentRequestReferrer(HttpRequest request)
		{
			if (request == null)
				return "No HttpContext or Request";

			Uri url = null;
			try
			{
				if (request.UrlReferrer != null)
					url = request.UrlReferrer;
			}
			catch
			{
			}

			if (url == null)
				return string.Empty;

			return url.ToString();
		}
		private static string CurrentRequestUserAgent(HttpRequest request)
		{
			if (request == null)
				return "No HttpContext or Request";

			return request.UserAgent;
		}
		private static string CurrentRequestUserAgentShort(HttpRequest request)
		{
			var userAgent = CurrentRequestUserAgent(request);
			if (userAgent == null)
				return null;

			var index = userAgent.IndexOf("(", StringComparison.Ordinal);
			if (index > -1)
				userAgent = userAgent.Substring(0, index);

			return userAgent.Trim();
		}
		private static string CurrentRequestServerId(HttpRequest request)
		{
			if (request == null)
				return "No HttpContext or Request";

			string serverId = null;
			try
			{
				if (request["ServerID"] != null)
					serverId = request["ServerID"];
			}
			catch
			{
			}

			return serverId ?? string.Empty;
		}
	}
}