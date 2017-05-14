using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using Web.DTO;
using Web.Hubs;
using Web.Service;

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
