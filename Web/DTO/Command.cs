namespace Web.DTO
{
    public class Command
    {
        public CommandType Type { get; set; }
    }

    public enum CommandType
    {
        Scrub,
        Notify,
        SubmitResponse,
        Instruct,
        Pay
    }
}
