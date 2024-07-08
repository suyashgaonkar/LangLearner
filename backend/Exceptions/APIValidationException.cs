using LangLearner.Models.Dtos.Responses;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LangLearner.Exceptions
{
    public class APIValidationException : GeneralAPIException
    {
        public IDictionary<string, string[]> Errors { get; }

        public APIValidationException(string message, IDictionary<string, string[]> Errors) : base(message)
        {
            this.StatusCode = 400;
            this.Errors = Errors;
        }
    }
}
