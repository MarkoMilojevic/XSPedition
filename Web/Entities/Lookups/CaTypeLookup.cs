using System.ComponentModel.DataAnnotations;

namespace Web.Entities.Lookups
{
	public class CaTypeLookup
	{
		[Key]
		public int CaTypeLookupId { get; set; }
		
		[Required]
		public string Code { get; set; }
	}
}