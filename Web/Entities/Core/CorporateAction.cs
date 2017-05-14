using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web.Entities.Lookups;

namespace Web.Entities.Core
{
	public class CorporateAction
	{
		[Key]
		public int CaId { get; set; }
		
		[Required]
		public int CaTypeLookupId { get; set; }

		[ForeignKey(nameof(CorporateAction.CaTypeLookupId))]
		public virtual CaTypeLookup CaTypeLookup { get; set; }
		
		public virtual List<Balance> Balances { get; set; }

		public virtual List<Option> Options { get; set; }
	}
}
