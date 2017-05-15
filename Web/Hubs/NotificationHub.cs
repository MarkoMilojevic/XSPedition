using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Web.DTO;
using Web.Entities.Shared;
using Web.Service;

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

            ScrubbingEventDto @event = CreateFirstCAScrubbingEvent();
            CaProcess data = _apiService.HandleEvent(@event);
            Clients.All.updateProcess(data);
            Thread.Sleep(sleepTime);
        }

        private ScrubbingEventDto CreateFirstCAScrubbingEvent()
        {
            return new ScrubbingEventDto
            {
                CaId = 1,
                CaTypeId = 1,
                EventDate = DateTime.Now,
                Fields = new Dictionary<int, string>
                {
                    { 1, "05/01/2017" },
                    { 2, "05/15/2017" }
                },
                Options = new List<OptionDto>
                {
                    new OptionDto
                    {
                        OptionNumber = 1,
                        OptionTypeId = 1,
                        Fields = new Dictionary<int, string>
                        {
                            { 1, "05/05/2017" },
                            { 2, null }
                        },
                        Payouts = new List<PayoutDto>
                        {
                            new PayoutDto
                            {
                                PayoutNumber = 1,
                                PayoutTypeId = 1,
                                Fields = new Dictionary<int, string>
                                {
                                    { 1, "07/01/2017" },
                                    { 2, null }
                                },
                            }
                        }
                    }
                }
            };
        }
    }
}
