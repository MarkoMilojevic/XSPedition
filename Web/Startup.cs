using Owin;
using Microsoft.Owin;
[assembly: OwinStartup(typeof(Web.Startup))]

namespace Web
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.MapSignalR();
		}
	}
}
