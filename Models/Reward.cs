namespace BetEngage.Api.Models
{
    public class Reward
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

    }
}