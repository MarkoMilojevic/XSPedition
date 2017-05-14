using System.ComponentModel.DataAnnotations;
using Web.Models.Shared;

namespace Web.Entities.Denormalized
{
	public class ScrubbingProcessView
	{
		[Key]
		public int ScrubbingProcessViewId { get; set; }

		public int? CaId { get; set; }

		public int? OptionId { get; set; }

		public int? PayoutId { get; set; }

		public int? FieldLookupId { get; set; }
		
		public string FieldDisplay { get; set; }

		public ProcessedDateCategory ProcessedDateCategory { get; set; }

		public bool IsSrubbed { get; set; }
	}
}
