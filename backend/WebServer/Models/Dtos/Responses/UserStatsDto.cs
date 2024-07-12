using System.ComponentModel.DataAnnotations;

namespace LangLearner.Models.Dtos.Responses
{
    public class UserStatsDto : UserDto
    {
        [Required]
        public int finishedCoursesCount { get; set; } = 0;

    }
}
