using LangLearner.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace LangLearner.Models.Dtos.Responses
{
    public class LanguageDto
    {
        [Required(ErrorMessage = "language code must be present")]
        [MaxLength(4)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Key]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string NativeName { get; set; } = string.Empty;

    }
}
