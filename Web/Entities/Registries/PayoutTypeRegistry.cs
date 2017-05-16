using System.ComponentModel.DataAnnotations;

namespace Web.Entities.Registries
{
	public class PayoutTypeRegistry
	{
		[Key]
		public int PayoutTypeRegistryId { get; set; }

		[Required]
		public string Code { get; set; }
	}
}