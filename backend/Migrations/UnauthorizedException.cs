using LangLearner.Exceptions;

namespace LangLearner.Migrations
{
    public class UnauthorizedException : GeneralAPIException
    {

        public UnauthorizedException(string message = "You are not authorize. Please try to login again") : base(message)
        {
            StatusCode = 401;
        }

    }
}
