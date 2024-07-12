using System.ComponentModel.DataAnnotations;

namespace LangLearner.Models.Entities
{
    /*public enum LanguageEnum
    {
        English,
        Spanish,
        French,
        German,
        Chinese,
        Japanese,
        Italian,
        Portuguese
    }

    public enum LanguageCodeEnum
    {
        EN, 
        ES,
        FR,
        DE,
        ZH,
        JA, 
        IT,
        PT 
    }*/

    public class Language
    {
        [Required(ErrorMessage = "language code must be present")]
        [MaxLength(4)]
        public string? Code { get; set; }

        [Required]
        [MaxLength(100)]
        [Key]
        public string? Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string? NativeName { get; set; }

        public ICollection<User>? NativeLanguageUsers { get; set; } = null;
        public ICollection<User>? AppLanguageUsers { get; set; } = null;
        // TODO: icon
    }
}
