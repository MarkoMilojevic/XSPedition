using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.DTO
{
	public class CaEvent
	{
		public int CaId { get; set; }

		public int ProcessTypeId { get; set; }

		public DateTime Date { get; set; }

		public int TargetId { get; set; }

		public bool IsProcessed { get; set; }
	}
}
