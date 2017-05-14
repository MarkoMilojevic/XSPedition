using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Entities.System
{
	public class EventLog
	{
		[Key]
		public int EventLogId { get; set; }

		public int CaId { get; set; }
		
		public int ProcessTypeLookupId { get; set; }

		public string EventLogInfo { get; set; }

		public DateTime Date { get; set; }
	}
}
