using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web.Entities.Registries;

namespace Web.Entities.Core
{
	public class Payout
	{
		[Key]
		public int PayoutId { get; set; }

		[Required]
		public int OptionId { get; set; }

		[Required]
		public int PayoutTypeRegistryId { get; set; }

		[Required]
		public int PayoutNumber { get; set; }

		[ForeignKey(nameof(Payout.PayoutTypeRegistryId))]
		public PayoutTypeRegistry PayoutType { get; set; }

		[ForeignKey(nameof(Payout.OptionId))]
		public virtual Option Option{ get; set; }
	}
}
