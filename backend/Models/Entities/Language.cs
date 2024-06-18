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
        [Required(ErrorMessage = "code should be there dude")]
        [MaxLength(4)]
        public string? Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string? NativeName { get; set; }
        // TODO: icon
    }
}
