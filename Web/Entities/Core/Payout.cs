using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web.Entities.Lookups;

namespace Web.Entities.Core
{
	public class Payout
	{
		[Key]
		public int PayoutId { get; set; }

		[Required]
		public int OptionId { get; set; }

		[Required]
		public int PayoutTypeLookupId { get; set; }

		[Required]
		public string PayoutNumber { get; set; }

		[ForeignKey(nameof(Payout.PayoutTypeLookupId))]
		public PayoutTypeLookup PayoutTypeLookup { get; set; }

		[ForeignKey(nameof(Payout.OptionId))]
		public virtual Option Option{ get; set; }
	}
}
