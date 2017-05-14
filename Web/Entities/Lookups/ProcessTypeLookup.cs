using System.ComponentModel.DataAnnotations;

namespace Web.Entities.Lookups
{
	public class ProcessTypeLookup
	{
		[Key]
		public int ProcessTypeLookupId { get; set; }

		[Required]
		public string Code { get; set; }

		[Required]
		public string Display { get; set; }
	}
}
