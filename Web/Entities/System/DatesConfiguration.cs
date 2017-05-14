using System.ComponentModel.DataAnnotations;

namespace Web.Entities.System
{
	public class DatesConfiguration
	{
		[Key]
		public int DateConfigurationId { get; set; }

		public int CaType { get; set; }

		public int ProcessTypeLookupId { get; set; }

		public int FieldLookupId { get; set; }

		public int DateOffset { get; set; }

		public bool IsCritical { get; set; }
	}
}
