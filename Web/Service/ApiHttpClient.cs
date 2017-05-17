using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Web.Service
{
	public class ApiHttpClient
	{
		public static HttpClient GetHttpClient(string requestedVersion = null)
		{
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUri"]);
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			return client;
		}
	}
}