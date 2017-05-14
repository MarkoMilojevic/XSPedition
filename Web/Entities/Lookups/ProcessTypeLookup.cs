using System.ComponentModel.DataAnnotations;
using Web.Entities.Shared;

namespace Web.Entities.Lookups
{
	public class ProcessTypeLookup
	{
		[Key]
		public int ProcessTypeLookupId { get; set; }

		[Required]
		public ProcessType Type { get; set; }

		[Required]
		public string Display { get; set; }
	}
}
