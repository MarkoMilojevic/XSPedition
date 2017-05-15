using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web.Entities.Lookups;

namespace Web.Entities.Core
{
	public class Option
	{
		[Key]
		public int OptionId { get; set; }

		[Required]
		public int CaId { get; set; }

		[Required]
		public int OptionTypeLookupId { get; set; }

		[Required]
		public int OptionNumber { get; set; }

		[ForeignKey(nameof(Option.OptionTypeLookupId))]
		public OptionTypeLookup OptionTypeLookup { get; set; }

		[ForeignKey(nameof(Option.CaId))]
		public virtual CorporateAction CorporateAction { get; set; }
		
		public virtual List<Payout> Payouts { get; set; }
	}
}
