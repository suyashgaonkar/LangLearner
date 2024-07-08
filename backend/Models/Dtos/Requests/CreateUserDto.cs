using LangLearner.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace LangLearner.Models.Dtos.Requests
{
    public class CreateUserDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string NativeLanguageName { get; set; } = string.Empty;

        public string AppLanguageName { get; set; } = string.Empty;
    }
}
