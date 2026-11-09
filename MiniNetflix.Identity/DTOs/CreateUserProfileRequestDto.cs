using System.ComponentModel.DataAnnotations;

namespace MiniNetflix.Identity.DTOs
{
    public class CreateUserProfileRequestDto
    {
        public Guid? AvatarId { get; set; }
        [MaxLength(256)]
        public string? FullName { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
