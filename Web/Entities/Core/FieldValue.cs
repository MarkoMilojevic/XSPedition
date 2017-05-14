using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web.Entities.Lookups;

namespace Web.Entities.Core
{
	public class FieldValue
	{
		[Key]
		public int FieldValueId { get; set; }
		
		[Required]
		public int FieldLookupId { get; set; }
		
		public bool IsScrubbed { get; set; }

		public int? CaId { get; set; }

		public int? OptionId { get; set; }

		public int? PayoutId { get; set; }

		[ForeignKey(nameof(FieldValue.FieldLookupId))]
		public virtual FieldLookup FieldLookup { get; set; }
	}
}
