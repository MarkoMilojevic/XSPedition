namespace Web.DTO
{
    public class EventDto
    {
        public EventType Type { get; set; }
    }

    public enum EventType
    {
        Scrubbing,
        Notification,
        Response,
        Instruction,
        Payment
    }
}
