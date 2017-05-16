using System.ComponentModel.DataAnnotations;

namespace Web.Entities.Registries
{
	public class OptionTypeRegistry
	{
		[Key]
		public int OptionTypeRegistryId { get; set; }

		[Required]
		public string Code { get; set; }
	}
}