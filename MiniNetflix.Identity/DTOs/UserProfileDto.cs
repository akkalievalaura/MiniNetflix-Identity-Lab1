namespace MiniNetflix.Identity.DTOs
{
    public class UserProfileDto
    {
        public Guid? AvatarId { get; set; }
        public string? FullName { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
