using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Web.Hubs
{
	[HubName("NotificationHub")]
	public class NotificationHub: Hub
	{
		private readonly List<Process> _processes;

		public NotificationHub()
		{
			_processes = new List<Process>
			{
				new Process { id = "scrub", title = "CA Scrubbing", targetDateItems = new List<string>(), criticalDateItems = new List<string>() },
				new Process { id = "notif", title = "Notification", targetDateItems = new List<string>(), criticalDateItems = new List<string>() },
				new Process { id = "respo", title = "Response", targetDateItems = new List<string>(), criticalDateItems = new List<string>() },
				new Process { id = "instr", title = "Instruction", targetDateItems = new List<string>(), criticalDateItems = new List<string>() },
				new Process { id = "payme", title = "Payment", targetDateItems = new List<string>(), criticalDateItems = new List<string>() }
			};
		}

		public void StartSimulation()
		{
			const int sleepTime = 1000;

			UpdateScrubbing(sleepTime);
			UpdateNotifications(sleepTime);
			UpdateResponses(sleepTime);
			UpdateInstructions(sleepTime);
			UpdatePayments(sleepTime);
		}

		private void UpdateScrubbing(int sleepTime)
		{
			_processes[0].targetDateItems.Add("Ex Date (IN)");
			Clients.All.update(_processes);
			Thread.Sleep(sleepTime);

			_processes[0].targetDateItems.Add("Amount(CT)");
			Clients.All.update(_processes);
			Thread.Sleep(sleepTime);

			_processes[0].criticalDateItems.Add("Rate(CO)");
			Clients.All.update(_processes);
			Thread.Sleep(sleepTime);
		}

		private void UpdateNotifications(int sleepTime)
		{
			_processes[1].targetDateItems.Add("Notification1");
			Clients.All.update(_processes);
			Thread.Sleep(sleepTime);

			_processes[1].targetDateItems.Add("Notification2");
			Clients.All.update(_processes);
			Thread.Sleep(sleepTime);

			_processes[1].targetDateItems.Add("Notification3");
			Clients.All.update(_processes);
			Thread.Sleep(sleepTime);

			_processes[1].targetDateItems.Add("Notification4");
			Clients.All.update(_processes);
			Thread.Sleep(sleepTime);

			_processes[1].criticalDateItems.Add("Notification5");
			Clients.All.update(_processes);
			Thread.Sleep(sleepTime);

			_processes[1].criticalDateItems.Add("Notification6");
			Clients.All.update(_processes);
			Thread.Sleep(sleepTime);

			_processes[1].criticalDateItems.Add("Notification7");
			Clients.All.update(_processes);
			Thread.Sleep(sleepTime);
		}

		private void UpdateResponses(int sleepTime)
		{
			_processes[2].targetDateItems.Add("acc1");
			Clients.All.update(_processes);
			Thread.Sleep(sleepTime);

			_processes[2].targetDateItems.Add("acc2");
			Clients.All.update(_processes);
			Thread.Sleep(sleepTime);

			_processes[2].targetDateItems.Add("acc3");
			Clients.All.update(_processes);
			Thread.Sleep(sleepTime);

			_processes[2].criticalDateItems.Add("acc4");
			Clients.All.update(_processes);
			Thread.Sleep(sleepTime);

			_processes[2].criticalDateItems.Add("acc5");
			Clients.All.update(_processes);
			Thread.Sleep(sleepTime);

			_processes[2].criticalDateItems.Add("acc6");
			Clients.All.update(_processes);
			Thread.Sleep(sleepTime);
		}

		private void UpdateInstructions(int sleepTime)
		{
			_processes[3].targetDateItems.Add("acc1");
			Clients.All.update(_processes);
			Thread.Sleep(sleepTime);

			_processes[3].targetDateItems.Add("acc2");
			Clients.All.update(_processes);
			Thread.Sleep(sleepTime);

			_processes[3].targetDateItems.Add("acc3");
			Clients.All.update(_processes);
			Thread.Sleep(sleepTime);

			_processes[3].targetDateItems.Add("acc4");
			Clients.All.update(_processes);
			Thread.Sleep(sleepTime);
			
			_processes[3].criticalDateItems.Add("acc5");
			Clients.All.update(_processes);
			Thread.Sleep(sleepTime);

			_processes[3].criticalDateItems.Add("acc6");
			Clients.All.update(_processes);
			Thread.Sleep(sleepTime);
		}

		private void UpdatePayments(int sleepTime)
		{
			_processes[4].targetDateItems.Add("acc1");
			Clients.All.update(_processes);
			Thread.Sleep(sleepTime);

			_processes[4].targetDateItems.Add("acc2");
			Clients.All.update(_processes);
			Thread.Sleep(sleepTime);

			_processes[4].criticalDateItems.Add("acc3");
			Clients.All.update(_processes);
			Thread.Sleep(sleepTime);

			_processes[4].criticalDateItems.Add("acc4");
			Clients.All.update(_processes);
			Thread.Sleep(sleepTime);
		}
	}

	public class Process
	{
		public string id { get; set; }

		public string title { get; set; }

		public List<string> targetDateItems { get; set; }

		public List<string> criticalDateItems { get; set; }
	}
}
