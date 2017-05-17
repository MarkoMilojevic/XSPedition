using Xspedition.Common;

namespace Web.Entities.Denormalized
{
	public class PaymentProcessView
	{
		public int PaymentProcessViewId { get; set; }

		public int CaId { get; set; }

		public int ResponseId { get; set; }

		public int PayoutId { get; set; }

		public string FieldDisplay { get; set; }

		public ProcessedDateCategory ProcessedDateCategory { get; set; }

		public bool IsSettled { get; set; }
	}
}
