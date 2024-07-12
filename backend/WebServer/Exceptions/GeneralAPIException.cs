namespace LangLearner.Exceptions
{
    public class GeneralAPIException : Exception
    {
        public int StatusCode { get; set; } = 500;
        public GeneralAPIException(string message) : base(message) { }
    }
}
