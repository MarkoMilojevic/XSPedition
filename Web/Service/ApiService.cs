using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Web.DTO;

namespace Web.Service
{
	public class ApiService
	{
		public CaProcess HandleEvent(CaEvent @event)
		{
			HttpClient httpClient = ApiHttpClient.GetHttpClient();

			string apiParams = BuildParams(@event);
			HttpResponseMessage response = httpClient.GetAsync($"api/events?{apiParams}").Result;
			if (!response.IsSuccessStatusCode)
			{
				return null;
			}

			string responseContent = response.Content.ReadAsStringAsync().Result;
			return JsonConvert.DeserializeObject<CaProcess>(responseContent);
		}

		private string BuildParams(CaEvent @event)
		{
			return $"caId={@event.CaId}&processTypeId={@event.ProcessTypeId}&date={@event.Date.ToShortDateString()}&targetId={@event.TargetId}&isProcessed={@event.IsProcessed}";
		}
	}
}