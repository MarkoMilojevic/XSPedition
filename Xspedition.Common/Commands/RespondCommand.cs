using System.Collections.Generic;
using Xspedition.Common.Dto;

namespace Xspedition.Common.Commands
{
    public class RespondCommand: Command
    {
        public List<ResponseDto> ResponseDto { get; set; }

        public RespondCommand(): base(CommandType.Respond)
        {
            ResponseDto = new List<ResponseDto>();
        }
    }
}
