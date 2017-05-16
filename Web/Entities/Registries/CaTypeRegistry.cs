using System.ComponentModel.DataAnnotations;

namespace Web.Entities.Registries
{
	public class CaTypeRegistry
	{
		[Key]
		public int CaTypeRegistryId { get; set; }
		
		[Required]
		public string Code { get; set; }
	}
}