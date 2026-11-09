namespace MiniNetflix.Identity.DTOs
{
    public class SubscriptionDto
    {
        public Guid UserId { get; set; }
        public string Level { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime? StartsAt { get; set; }
        public DateTime? EndsAt { get; set; }
    }
}
