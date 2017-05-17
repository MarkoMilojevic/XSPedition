using System.ComponentModel.DataAnnotations;
using Xspedition.Common;

namespace Web.Entities.Denormalized
{
	public class NotificationProcessView
	{
		[Key]
		public int NotificationProcessViewId { get; set; }

		public int CaId { get; set; }

		public int BalanceId { get; set; }

		public string FieldDisplay { get; set; }

		public ProcessedDateCategory ProcessedDateCategory { get; set; }

		public bool IsNotificationSent { get; set; }
	}
}
