using System.ComponentModel.DataAnnotations;

namespace Web.Entities.Lookups
{
	public class FieldLookup
	{
		[Key]
		public int FieldLookupId { get; set; }

		[Required]
		public string FieldDisplay { get; set; }

		[Required]
		public string FieldType { get; set; }

		[Required]
		public bool IsRequired { get; set; }

		public int? CaTypeLookupId { get; set; }

		public int? OptionTypeLookupId { get; set; }

		public int? PayoutTypeLookupId { get; set; }
	}
}
