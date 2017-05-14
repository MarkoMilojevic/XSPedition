using Web.Models.Shared;

namespace Web.Entities.Denormalized
{
	public class InstructionProcessView
	{
		public int InstructionProcessViewId { get; set; }

		public int CaId { get; set; }

		public int ResponseId { get; set; }

		public string FieldDisplay { get; set; }

		public ProcessedDateCategory ProcessedDateCategory { get; set; }

		public bool IsInstructed { get; set; }
	}
}
