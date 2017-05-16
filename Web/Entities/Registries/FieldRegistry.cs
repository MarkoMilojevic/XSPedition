using System.ComponentModel.DataAnnotations;

namespace Web.Entities.Registries
{
	public class FieldRegistry
	{
		[Key]
		public int FieldRegistryId { get; set; }

		[Required]
		public string FieldDisplay { get; set; }

		[Required]
		public string FieldType { get; set; }
	}
}
