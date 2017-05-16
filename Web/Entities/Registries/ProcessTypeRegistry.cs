using System.ComponentModel.DataAnnotations;
using Web.Entities.Shared;

namespace Web.Entities.Registries
{
	public class ProcessTypeRegistry
	{
		[Key]
		public int ProcessTypeRegistryId { get; set; }

		[Required]
		public ProcessType Type { get; set; }

		[Required]
		public string Display { get; set; }
	}
}
