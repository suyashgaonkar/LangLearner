namespace LangLearner.Models.Auth
{
    public class TokenClaims
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
