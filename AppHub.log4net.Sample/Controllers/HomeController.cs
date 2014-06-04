using System;
using System.Web.Mvc;
using log4net;

namespace AppHub.Log4net.Sample.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult LogError()
		{
			var logger = LogManager.GetLogger(typeof (HomeController));
			logger.Warn("Warn logger");
			try
			{
				throw new Exception("Fatal error - " + DateTime.Now);
			}
			catch (Exception ex)
			{
				logger.Fatal(ex.Message, ex);
			}

			return RedirectToAction("Index");
		}
	}
}