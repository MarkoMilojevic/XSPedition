using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web.Entities.Registries;

namespace Web.Entities.Core
{
	public class CorporateAction
	{
		[Key]
		public int CaId { get; set; }
		
		[Required]
		public int CaTypeRegistryId { get; set; }

		[ForeignKey(nameof(CorporateAction.CaTypeRegistryId))]
		public virtual CaTypeRegistry CaType { get; set; }
		
		public virtual List<Balance> Balances { get; set; }

		public virtual List<Option> Options { get; set; }
	}
}
