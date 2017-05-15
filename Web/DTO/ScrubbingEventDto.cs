using System;
using System.Collections.Generic;

namespace Web.DTO
{
    public class ScrubbingEventDto : EventDto
    {
        public int CaId { get; set; }

        public int? CaTypeId { get; set; }

        public DateTime EventDate { get; set; }

        public Dictionary<int, string> Fields { get; set; }

        public List<OptionDto> Options { get; set; }

        public ScrubbingEventDto()
        {
            this.Fields = new Dictionary<int, string>();
            this.Options = new List<OptionDto>();
        }
    }

    public class OptionDto
    {
        public int OptionNumber { get; set; }

        public int? OptionTypeId { get; set; }

        public Dictionary<int, string> Fields { get; set; }

        public List<PayoutDto> Payouts { get; set; }

        public OptionDto()
        {
            this.Fields = new Dictionary<int, string>();
            this.Payouts = new List<PayoutDto>();
        }
    }

    public class PayoutDto
    {
        public int PayoutNumber { get; set; }

        public int? PayoutTypeId { get; set; }

        public Dictionary<int, string> Fields { get; set; }

        public PayoutDto()
        {
            this.Fields = new Dictionary<int, string>();
        }
    }
}
