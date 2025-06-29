namespace BetEngage.Api.Models
{
    public class UserEvent
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Event { get; set; } = string.Empty;
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

    }
}