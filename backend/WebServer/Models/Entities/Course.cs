using System.ComponentModel.DataAnnotations;

namespace LangLearner.Models.Entities
{
    public class Course
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CreatorId { get; set; }

        [Required]

        public int ReportsCount { get; set; } = 0;

        [Required]
        [MinLength(10)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(5000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string TargetLanguageName { get; set; } = string.Empty;

        [Required]
        public bool Verified { get; set; } = false;

        [Required]
        public int LikesCount { get; set; } = 0;

        [Required]
        public int DislikesCount { get; set; } = 0;

        public virtual Language? TargetLanguage { get; set; }

        public virtual User? Creator { get; set; }

        public virtual IEnumerable<User>? EnrolledUsers { get; set; }


    }
}
