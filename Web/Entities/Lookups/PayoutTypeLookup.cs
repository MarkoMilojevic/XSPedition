using System.ComponentModel.DataAnnotations;

namespace Web.Entities.Lookups
{
	public class PayoutTypeLookup
	{
		[Key]
		public int PayoutTypeLookupId { get; set; }

		[Required]
		public string Code { get; set; }
	}
}