using System.ComponentModel.DataAnnotations;

namespace LangLearner.Models.Dtos.Requests
{
    public class LoginUserDto
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
