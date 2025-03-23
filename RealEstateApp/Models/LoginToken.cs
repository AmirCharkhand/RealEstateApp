namespace RealEstateApp.Models
{
    public class LoginToken (string accessToken, int userId, string userName)
    {
        public string AccessToken { get; init; } = accessToken;
        public int UserId{ get; init; } = userId;
        public string UserName { get; init; } = userName;
    }
}
