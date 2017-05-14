using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Entities.Core
{
	public class Balance
	{
		[Key]
		public int BalanceId { get; set; }

		public int CaId { get; set; }

		public int AccountId { get; set; }

		public bool IsNotificationSent { get; set; }

		[ForeignKey(nameof(Balance.CaId))]
		public virtual CorporateAction CorporateAction { get; set; }

		[ForeignKey(nameof(Balance.AccountId))]
		public virtual Account Account { get; set; }
	}
}
