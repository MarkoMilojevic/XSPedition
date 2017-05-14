using System.Collections.Generic;

namespace Web.DTO
{
	public class CaProcess
	{
		public string id { get; set; }

		public string title { get; set; }

		public List<string> targetDateItems { get; set; }

		public List<string> criticalDateItems { get; set; }
	}
}