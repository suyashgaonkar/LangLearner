namespace LangLearner.Models.Dtos.Responses
{
    public class ApiValidationError : ApiError
    {
        public IDictionary<string, string[]>? Errors { get; set; } = null;

    }
}
