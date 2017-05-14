using System;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using Web.Hubs;

namespace Web.Controllers
{
	public class SignalController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}
