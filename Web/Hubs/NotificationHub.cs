using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Web.DTO;
using Web.Service;
using Web.ViewModels;

namespace Web.Hubs
{
    [HubName("NotificationHub")]
    public class NotificationHub : Hub
    {
        private readonly ApiService _apiService;

        public NotificationHub()
        {
            _apiService = new ApiService();
        }

        public void StartSimulation()
        {
            const int sleepTime = 1000;

            ScrubCaCommand command = CreateFirstCAScrubbingEvent();
            CaProcessViewModel viewModel = _apiService.Execute(command);
	        if (viewModel != null)
	        {
				Clients.All.updateProcess(viewModel);
				Thread.Sleep(sleepTime);
	        }
		}

        private ScrubCaCommand CreateFirstCAScrubbingEvent()
        {
            return new ScrubCaCommand
            {
                CaId = 1,
                CaTypeId = 1,
                EventDate = DateTime.Now,
                Fields = new Dictionary<int, string>
                {
                    { 1, "05/01/2017" },
                    { 4, "05/15/2017" }
                },
                Options = new List<OptionDto>
                {
                    new OptionDto
                    {
                        OptionNumber = 1,
                        OptionTypeId = 1,
                        Fields = new Dictionary<int, string>
                        {
                            { 101, "05/05/2017" },
                            { 103, null }
                        },
                        Payouts = new List<PayoutDto>
                        {
                            new PayoutDto
                            {
                                PayoutNumber = 1,
                                PayoutTypeId = 1,
                                Fields = new Dictionary<int, string>
                                {
                                    { 1009, "07/01/2017" },
                                    { 1010, null }
                                },
                            }
                        }
                    }
                }
            };
        }
    }
}
