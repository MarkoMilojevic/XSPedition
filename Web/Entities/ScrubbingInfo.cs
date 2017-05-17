using System.ComponentModel.DataAnnotations;
using Web.Models.Shared;

namespace Web.Entities
{
	public class ScrubbingInfo
	{
		[Key]
		public int ScrubbingId { get; set; }

		public int CaId { get; set; }

		public int? CaTypeId { get; set; }

		public int? OptionNumber { get; set; }

        public int? OptionTypeId { get; set; }

		public int? PayoutNumber { get; set; }

	    public int? PayoutTypeId { get; set; }

		public string FieldDisplay { get; set; }

		public ProcessedDateCategory ProcessedDateCategory { get; set; }

		public bool IsSrubbed { get; set; }
	}
}
