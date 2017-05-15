using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Web.DTO;
using Web.ViewModels;

namespace Web.Service
{
	public class ApiService
	{
		public CaProcessViewModel Execute(Command command)
		{
			CaProcessViewModel result = null;

			switch (command.Type)
			{
				case CommandType.Scrub:
					ScrubCaCommand scrubCaCommand = (ScrubCaCommand) command;
					ExecuteScrubCa(scrubCaCommand);

					CaScrubbedEvent caScrubbedEvent = new CaScrubbedEvent {CaId = scrubCaCommand.CaId, Date = scrubCaCommand.EventDate};
					result = HandleScrubCaEvent(caScrubbedEvent);
					break;
				case CommandType.Notify:
					break;
				case CommandType.SubmitResponse:
					break;
				case CommandType.Instruct:
					break;
				case CommandType.Pay:
					break;
			}

			return result;
		}

		public void ExecuteScrubCa(ScrubCaCommand scrubbingEvent)
	    {
	        HttpClient httpClient = ApiHttpClient.GetHttpClient();

	        string serializedEvent = JsonConvert.SerializeObject(scrubbingEvent);
	        StringContent httpContent = new StringContent(serializedEvent, Encoding.Unicode, "application/json");

			HttpResponseMessage response = httpClient.PostAsync("api/events/scrubbingcommand", httpContent).Result;
	    }

		private CaProcessViewModel HandleScrubCaEvent(CaScrubbedEvent caScrubbedEvent)
		{
			HttpClient httpClient = ApiHttpClient.GetHttpClient();

			string serializedEvent = JsonConvert.SerializeObject(caScrubbedEvent);
			StringContent httpContent = new StringContent(serializedEvent, Encoding.Unicode, "application/json");

			HttpResponseMessage response = httpClient.PostAsync("api/events/scrubbingevent", httpContent).Result;
			if (!response.IsSuccessStatusCode)
			{
				return null;
			}

			response = httpClient.GetAsync($"api/events/scrubbing/{caScrubbedEvent.CaId}").Result;
			if (!response.IsSuccessStatusCode)
			{
				return null;
			}

			string responseContent = response.Content.ReadAsStringAsync().Result;
			return JsonConvert.DeserializeObject<CaProcessViewModel>(responseContent);
		}
	}
}
