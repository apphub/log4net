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
			logger.Fatal("Fatal error - " + DateTime.Now);

			return RedirectToAction("Index");
		}
	}
}