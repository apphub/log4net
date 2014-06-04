using System.Web;
using System.Web.Mvc;

namespace AppHub.Log4net.Sample
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
