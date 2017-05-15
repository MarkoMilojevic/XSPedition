using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Web.DTO;

namespace Web.Service
{
	public class ApiService
	{
		public CaProcess HandleEvent(EventDto @event)
		{
		    switch (@event.Type)
		    {
		        case EventType.Scrubbing:
		            return HandleScrubbingEvent((ScrubbingEventDto) @event);
		        case EventType.Notification:
		            break;
		        case EventType.Response:
		            break;
		        case EventType.Instruction:
		            break;
		        case EventType.Payment:
		            break;
		    }

            return null;
		}

	    public CaProcess HandleScrubbingEvent(ScrubbingEventDto scrubbingEvent)
	    {
	        HttpClient httpClient = ApiHttpClient.GetHttpClient();

	        string serializedEvent = JsonConvert.SerializeObject(scrubbingEvent);
	        StringContent httpContent = new StringContent(serializedEvent, Encoding.Unicode, "application/json");
            
	        HttpResponseMessage response = httpClient.PostAsync("api/events/scrubbing", httpContent).Result;
	        if (!response.IsSuccessStatusCode)
	        {
	            return null;
	        }
            
            response = httpClient.GetAsync($"api/events/scrubbing/?caId={scrubbingEvent.CaId}").Result;
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string responseContent = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<CaProcess>(responseContent);
        }
	}
}
