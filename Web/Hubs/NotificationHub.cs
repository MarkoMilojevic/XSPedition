using System;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Web.DTO;
using Web.Entities.Shared;
using Web.Service;

namespace Web.Hubs
{
	[HubName("NotificationHub")]
	public class NotificationHub: Hub
	{
		private readonly ApiService _apiService;

		public NotificationHub()
		{
			_apiService = new ApiService();
		}

		public void StartSimulation()
		{
			const int sleepTime = 1000;

			CaEvent event1 = new CaEvent
			{
				CaId = 1,
				ProcessTypeId = (int) ProcessType.Scrubbing,
				Date = DateTime.Now,
				TargetId = 1,
				IsProcessed = true
			};


			CaProcess data = _apiService.HandleEvent(event1);
			Clients.All.updateProcess(data);
			Thread.Sleep(sleepTime);

			CaEvent event2 = new CaEvent
			{
				CaId = 1,
				ProcessTypeId = (int)ProcessType.Scrubbing,
				Date = DateTime.Now,
				TargetId = 1,
				IsProcessed = false
			};

			data = _apiService.HandleEvent(event2);
			Clients.All.updateProcess(data);
			Thread.Sleep(sleepTime);

			CaEvent event3 = new CaEvent
			{
				CaId = 1,
				ProcessTypeId = (int)ProcessType.Scrubbing,
				Date = DateTime.Now,
				TargetId = 1,
				IsProcessed = true
			};

			data = _apiService.HandleEvent(event3);
			Clients.All.updateProcess(data);
			Thread.Sleep(sleepTime);
		}
	}
}
