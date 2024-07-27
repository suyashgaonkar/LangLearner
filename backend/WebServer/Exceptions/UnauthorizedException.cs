namespace LangLearner.Exceptions
{
    public class UnauthorizedException : GeneralAPIException
    {

        public UnauthorizedException(string message = "You are not authorize. Please try to login again") : base(message)
        {
            StatusCode = 401;
        }

    }
}
