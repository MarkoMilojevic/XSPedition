using Web.Models.Shared;

namespace Web.Entities.Denormalized
{
	public class ResponseProcessView
	{
		public int ResponseProcessViewId { get; set; }

		public int CaId { get; set; }

		public int BalanceId { get; set; }

		public string FieldDisplay { get; set; }

		public ProcessedDateCategory ProcessedDateCategory { get; set; }

		public bool IsSubmitted { get; set; }
	}
}
