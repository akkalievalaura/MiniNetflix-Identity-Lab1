using System.ComponentModel.DataAnnotations;
namespace MiniNetflix.Identity.DTOs
{
    public class ChangeSubscriptionRequestDto
    {
        [Required]
        public string Level { get; set; } = string.Empty;
    }
}
