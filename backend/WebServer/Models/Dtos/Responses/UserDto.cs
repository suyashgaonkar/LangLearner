using System.ComponentModel.DataAnnotations;

namespace LangLearner.Models.Dtos.Responses
{
    public class UserDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string NativeLanguageName { get; set; } = string.Empty;

        [Required]
        public string AppLanguageName { get; set; } = string.Empty;
    }
}
