using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Entities.Core
{
	public class Response
	{
		[Key, ForeignKey(nameof(Core.Balance))]
		public int ResponseId { get; set; }
		
		public bool IsInstructed { get; set; }
		
		[Required]
		public virtual Balance Balance { get; set; }
	}
}
