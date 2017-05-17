using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Web.DTO;
using Web.ViewModels;
using Xspedition.Common.Commands;

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

					result = HandleScrubCaEvent(scrubCaCommand.CaId);
					break;
				case CommandType.Notify:
                    NotifyCommand notifyCommand = (NotifyCommand)command;
                    ExecuteSendNotification(notifyCommand);

                    result = HandleSendNotificationEvent(notifyCommand.CaId);
                    break;
				case CommandType.Respond:
					break;
				case CommandType.Instruct:
                    InstructCommand instructCommand = (InstructCommand)command;
                    ExecuteInstruction(instructCommand);

                    result = HandleInstructionEvent(instructCommand.CaId);
                    break;
				case CommandType.Pay:
					break;
			}

			return result;
		}

		public void ExecuteScrubCa(ScrubCaCommand command)
	    {
            HttpClient httpClient = ApiHttpClient.GetHttpClient();

            string serializedEvent = JsonConvert.SerializeObject(command);
            StringContent httpContent = new StringContent(serializedEvent, Encoding.Unicode, "application/json");

            HttpResponseMessage response = httpClient.PostAsync("api/events/scrubbing", httpContent).Result;
        }

		private CaProcessViewModel HandleScrubCaEvent(int caId)
		{
            HttpClient httpClient = ApiHttpClient.GetHttpClient();
            
            HttpResponseMessage response = httpClient.GetAsync($"api/events/scrubbing/{caId}").Result;
			if (!response.IsSuccessStatusCode)
			{
				return null;
			}

			string responseContent = response.Content.ReadAsStringAsync().Result;
			return JsonConvert.DeserializeObject<CaProcessViewModel>(responseContent);
		}

        public void ExecuteSendNotification(NotifyCommand command)
        {
            HttpClient httpClient = ApiHttpClient.GetHttpClient();

            string serializedEvent = JsonConvert.SerializeObject(command);
            StringContent httpContent = new StringContent(serializedEvent, Encoding.Unicode, "application/json");

            HttpResponseMessage response = httpClient.PostAsync("api/events/notifications", httpContent).Result;
        }

        private CaProcessViewModel HandleSendNotificationEvent(int caId)
        {
            HttpClient httpClient = ApiHttpClient.GetHttpClient();

            HttpResponseMessage response = httpClient.GetAsync($"api/events/notifications/{caId}").Result;
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string responseContent = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<CaProcessViewModel>(responseContent);
        }

        public void ExecuteInstruction(InstructCommand command)
        {
            HttpClient httpClient = ApiHttpClient.GetHttpClient();

            string serializedEvent = JsonConvert.SerializeObject(command);
            StringContent httpContent = new StringContent(serializedEvent, Encoding.Unicode, "application/json");

            HttpResponseMessage response = httpClient.PostAsync("api/events/instructions", httpContent).Result;
        }

        private CaProcessViewModel HandleInstructionEvent(int caId)
        {
            HttpClient httpClient = ApiHttpClient.GetHttpClient();

            HttpResponseMessage response = httpClient.GetAsync($"api/events/instructions/{caId}").Result;
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string responseContent = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<CaProcessViewModel>(responseContent);
        }
    }
}
