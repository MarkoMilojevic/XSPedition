using System.ComponentModel.DataAnnotations;

namespace Web.Entities.Lookups
{
	public class OptionTypeLookup
	{
		[Key]
		public int OptionTypeLookupId { get; set; }

		[Required]
		public string Code { get; set; }
	}
}