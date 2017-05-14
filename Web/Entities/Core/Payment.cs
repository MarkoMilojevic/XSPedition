using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Entities.Core
{
	public class Payment
	{
		[Key]
		public int PaymentId { get; set; }

		public int ResponseId { get; set; }

		public int PayoutId { get; set; }

		public bool IsSettled { get; set; }

		[ForeignKey(nameof(Payment.ResponseId))]
		public virtual Response Response{ get; set; }

		[ForeignKey(nameof(Payment.PayoutId))]
		public virtual Payout Payout { get; set; }
	}
}
